using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.CrossPlatformInput;
using Assets.Pixelation.Scripts;
using MEC;

public enum CameraMode
{
    ThreeDimensional, TwoDimensional
}

public abstract class GameCamera : PivotBasedCameraRig {
    
    public static GameCamera Instance { get; protected set; }

    public Camera Camera { get; protected set; }
    public bool Animating { get { return pixelation.enabled; } }
    public Vector3 LookDirection { get { return transform.forward; } }

    [Header("Transition")]
    [SerializeField] protected float transitionTime = 1.0f;
    [SerializeField] protected float transitionBlackRatio = 0.75f;
    protected Pixelation pixelation;


    [HideInInspector] public UnityEvent onTransitionComplete;
    protected static int pixelCount2D = 256, pixelCountMax = 512;
    
    protected override void Awake()
    {
        base.Awake();
        Camera = GetComponentInChildren<Camera>();
        pixelation = Camera.GetComponent<Pixelation>();
    }

    protected virtual void OnEnable()
    {
        Instance = this;
    }

    public abstract void HandleUpdate();
    
    public void TransitionIn()
    {
        Timing.RunCoroutine(_AnimateTransitionIn());
    }
    
    public void TransitionOut()
    {
        Timing.RunCoroutine(_AnimateTransitionOut());
    }

    public void GoToTarget()
    {
        FollowTarget(100.0f);
    }

    private IEnumerator<float> _AnimateTransitionIn()
    {
        // pixelation.enabled = true;
        
        // Pixel in
        var t = 0.0f;
        while (t < transitionTime)
        {
            t += Time.deltaTime;
            var ratio = t / transitionTime;
            // pixelation.BlockCount = Mathf.Lerp(pixelCountMax, pixelCount2D, ratio);
            BlackScreen.Instance.SetAlpha(ratio);
            yield return 0;
        }
        onTransitionComplete.Invoke();
        onTransitionComplete.RemoveAllListeners();
    }


    private IEnumerator<float> _AnimateTransitionOut()
    {
        // Pixel in
        var t = 0.0f;
        while (t < transitionTime)
        {
            t += Time.deltaTime;
            var ratio = t / transitionTime;
            // pixelation.BlockCount = Mathf.Lerp(pixelCount2D, pixelCountMax, ratio);
            BlackScreen.Instance.SetAlpha(1.0f - ratio);
            yield return 0;
        }
        // pixelation.enabled = false;
        onTransitionComplete.Invoke();
        onTransitionComplete.RemoveAllListeners();
    }
}
