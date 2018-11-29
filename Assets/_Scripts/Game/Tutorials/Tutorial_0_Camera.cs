using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using MEC;
using UnityStandardAssets.Cameras;

public class Tutorial_0_Camera : Tutorial {
    
    [SerializeField] private float targetVignetteSmoothness = 1.0f, defaultVignetteSmoothness = 0.2f,
        animationTime = 1.0f;
    [SerializeField] private Transform lookTarget;
    [SerializeField] private Vector3 targetLookRot = new Vector3(-6, 180, 0);
    [SerializeField] private Vector3 targetPivotAngles = new Vector3(-6, 180, 0);
    private float cameraDist;
    private PostProcessingBehaviour pp;
    private GameCamera gameCamera;
    private Camera mainCamera;
    private bool targetVisible;
    private ProtectCameraFromWallClip clipScript;

	void Start ()
    {
        BeginTutorial();
	}

    public override void BeginTutorial()
    {
        base.BeginTutorial();

        // Init settings
        GameManager.Instance.state = GameState.Event;

        mainCamera = GameManager.Instance.Camera;
        gameCamera = GameManager.Instance.gameCamera;
        pp = mainCamera.GetComponent<PostProcessingBehaviour>();

        // Set vignette
        defaultVignetteSmoothness = pp.profile.vignette.settings.smoothness;
        SetVignette(targetVignetteSmoothness);

        // Set camera position
        clipScript = gameCamera.GetComponent<ProtectCameraFromWallClip>();
        cameraDist = mainCamera.transform.localPosition.magnitude;
        mainCamera.transform.localPosition = Vector3.zero;
        clipScript.SetDist(0);

        // Hide player
        GameManager.Instance.player.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        SetVignette(defaultVignetteSmoothness);
    }

    private void SetVignette(float smoothness)
    {
        var settings = pp.profile.vignette.settings;
        settings.smoothness = smoothness;
        pp.profile.vignette.settings = settings;
    }
	
	void Update ()
    {
        if (targetVisible)
            return;

        // Allow camera movement
        mainCamera.transform.localPosition = Vector3.zero;
        GameManager.Instance.gameCamera.HandleUpdate();
        
        // Check if target visible
        var vPos = mainCamera.WorldToViewportPoint(lookTarget.transform.position);
        if (vPos.x >= 0.3f && vPos.x <= 0.7f && vPos.y >= 0.3f && vPos.y <= 0.7f && vPos.z > 0)
        {
            targetVisible = true;
            Timing.RunCoroutine(_CameraLookAtTarget(), Segment.LateUpdate);
        }
	}

    private IEnumerator<float> _CameraLookAtTarget()
    {
        // Show player
        GameManager.Instance.player.gameObject.SetActive(true);

        // Animate
        var t = 0.0f;
        var lookDirection = mainCamera.transform.forward;
        var currentLook = gameCamera.GetLookRot();
        var currentPivot = gameCamera.GetPivotRot();

        while (t < animationTime)
        {
            t += Time.deltaTime;
            var ratio = t / animationTime;

            // Vignette
            SetVignette(Mathf.Lerp(targetVignetteSmoothness, defaultVignetteSmoothness, ratio));

            // Camera look
            var newRot = Quaternion.Lerp(currentLook, Quaternion.Euler(targetLookRot), ratio);
            var newPivotRot = Quaternion.Lerp(currentPivot, Quaternion.Euler(targetPivotAngles), ratio);
            gameCamera.SetLook(newRot, newPivotRot.eulerAngles);
            gameCamera.ApplyRotation();

            // Camera pos
            var newDist = Mathf.Lerp(0, cameraDist, ratio);
            clipScript.SetDist(newDist);

            GameManager.Instance.player.transform.LookAt(mainCamera.transform.forward);
            yield return 0;
        }
        GameManager.Instance.state = GameState.Idle;
        EndTutorial();
    }
}
