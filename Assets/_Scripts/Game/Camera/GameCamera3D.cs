using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.CrossPlatformInput;
using Assets.Pixelation.Scripts;
using MEC;

public class GameCamera3D : GameCamera {

    [Header("3D")]
    [SerializeField] private float m_MoveSpeed = 1f;                      // How fast the rig will move to keep up with the target's position.
    [Range(0f, 10f)] [SerializeField] private float m_TurnSpeed = 1.5f;   // How fast the rig will rotate from user input.
    [SerializeField] private float m_TurnSmoothing = 0.0f;                // How much smoothing to apply to the turn input, to reduce mouse-turn jerkiness
    [SerializeField] private float m_TiltMax = 75f;                       // The maximum value of the x axis rotation of the pivot.
    [SerializeField] private float m_TiltMin = 45f;                       // The minimum value of the x axis rotation of the pivot.
    [SerializeField] private bool m_LockCursor = false;                   // Whether the cursor should be hidden and locked.
    [SerializeField] private bool m_VerticalAutoReturn = false;           // set wether or not the vertical axis should auto return
    [SerializeField] private Vector3 rot3D;

    private float m_LookAngle;                    // The rig's y axis rotation.
    private float m_TiltAngle;                    // The pivot's x axis rotation.
    private const float k_LookDistance = 100f;    // How far in front of the pivot the character's look target is.
    private Vector3 m_PivotEulers;
    private Quaternion m_PivotTargetRot;
    private Quaternion m_TransformTargetRot;
    private Player player;

    [Header("Hide Target")]
    [SerializeField] private float hideTargetDist = 0.5f;
    [SerializeField] private LayerMask playerLayer;

    public CameraMode Mode { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        // Lock or unlock the cursor.
        Cursor.lockState = m_LockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !m_LockCursor;
        m_PivotEulers = m_Pivot.localRotation.eulerAngles;
        m_PivotTargetRot = m_Pivot.localRotation;
        m_LookAngle = -m_PivotEulers.y;
        m_TransformTargetRot = transform.localRotation;
        Mode = CameraMode.ThreeDimensional;
        player = m_Target.GetComponent<Player>();
    }

    public override void HandleUpdate()
    {
        if (player.playerState == PlayerState.Transition)
        {
            GameManager.Instance.UnlockCursor();
            ApplyRotation();
            return;
        }

        if (Mode == CameraMode.ThreeDimensional)
        {
            HandleRotationMovement();
        }

        if (m_LockCursor && Input.GetMouseButtonUp(0))
        {
            GameManager.Instance.LockCursor(m_LockCursor);
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.UnlockCursor();
    }

    protected override void FollowTarget(float deltaTime)
    {
        if (m_Target == null) return;
        // Move the rig towards target position.
        transform.position = Vector3.Lerp(transform.position, m_Target.position, deltaTime * m_MoveSpeed);

        if (player)
            player.SetVisible(Physics.OverlapSphere(m_Cam.position, hideTargetDist, playerLayer).Length == 0);
    }

    private void HandleRotationMovement()
    {
        if (Time.timeScale < float.Epsilon)
            return;

        // Read the user input
        var x = CrossPlatformInputManager.GetAxis("Mouse X");
        var y = CrossPlatformInputManager.GetAxis("Mouse Y");

        // Adjust the look angle by an amount proportional to the turn speed and horizontal input.
        m_LookAngle += x * m_TurnSpeed;
        
        if (m_VerticalAutoReturn)
        {
            // For tilt input, we need to behave differently depending on whether we're using mouse or touch input:
            // on mobile, vertical input is directly mapped to tilt value, so it springs back automatically when the look input is released
            // we have to test whether above or below zero because we want to auto-return to zero even if min and max are not symmetrical.
            m_TiltAngle = y > 0 ? Mathf.Lerp(0, -m_TiltMin, y) : Mathf.Lerp(0, m_TiltMax, -y);
        }
        else
        {
            // on platforms with a mouse, we adjust the current angle based on Y mouse input and turn speed
            m_TiltAngle -= y * m_TurnSpeed;
            // and make sure the new value is within the tilt range
            m_TiltAngle = Mathf.Clamp(m_TiltAngle, -m_TiltMin, m_TiltMax);
        }

        ApplyRotation();
    }

    public override void ApplyRotation()
    {
        // Rotate the rig (the root object) around Y axis only:
        m_TransformTargetRot = Quaternion.Euler(0f, m_LookAngle, 0f);

        // Tilt input around X is applied to the pivot (the child of this object)
        m_PivotTargetRot = Quaternion.Euler(m_TiltAngle, m_PivotEulers.y, m_PivotEulers.z);

        if (m_TurnSmoothing > 0)
        {
            m_Pivot.localRotation = Quaternion.Slerp(m_Pivot.localRotation, m_PivotTargetRot, m_TurnSmoothing * Time.deltaTime);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, m_TransformTargetRot, m_TurnSmoothing * Time.deltaTime);
        }
        else
        {
            m_Pivot.localRotation = m_PivotTargetRot;
            transform.localRotation = m_TransformTargetRot;
        }
    }

    public override void SetLook(Quaternion lookRot, Vector3 pivotEulers)
    {
        var euler = lookRot.eulerAngles;
        m_LookAngle = euler.y;
        m_PivotEulers = pivotEulers;
    }

    public override void StartAnimate(Quaternion lookRot, Vector3 pivotEulers, float time)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateTransitionPreview(lookRot, pivotEulers, time));
    }

    private IEnumerator AnimateTransitionPreview(Quaternion lookRot, Vector3 pivotEulers, float time)
    {
        float t = 0.0f;

        Quaternion startRot = transform.localRotation;
        Vector3 startEulers = m_PivotEulers;

        while(t < time)
        {
            t += Time.deltaTime;

            float ratio = t / time;
            SetLook(Quaternion.Lerp(startRot, lookRot, ratio), Vector3.Lerp(startEulers, pivotEulers, ratio));
            yield return null;
        }
    }

    public override Quaternion GetPivotRot()
    {
        return Quaternion.Euler(m_PivotEulers);
    }

    public override Quaternion GetLookRot()
    {
        return Quaternion.Euler(m_TiltAngle, m_LookAngle, m_PivotEulers.z);
    }
}
