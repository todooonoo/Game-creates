using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class Tutorial_2_Perspective : Tutorial
{
    public float animationTime = 1.0f;
    public DraggableTalk talkEvent;
    public Vector3 targetLookRot = new Vector3(-6, 180, 0),
        targetPivotAngles = new Vector3(-6, 180, 0);

    private GameCamera gameCamera;
    private Camera mainCamera;
    private int eventIndex;

    private IEnumerator Start()
    {
        yield return null;
        BeginTutorial();
    }

    public override void BeginTutorial()
    {
        base.BeginTutorial();

        if (!tutorialActive)
            return;

        // Init settings
        GameManager.Instance.state = GameState.Event;

        mainCamera = GameManager.Instance.Camera;
        gameCamera = GameManager.Instance.gameCamera;
        talkEvent.OnDrag(GameManager.Instance.player.transform, false);
        eventIndex = 1;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsIdle)
            return;

        if(eventIndex == 1)
        {
            eventIndex = 2;
            GameManager.Instance.state = GameState.Event;
            Timing.RunCoroutine(_CameraLookAtTarget());
        } else if(eventIndex == 3) {
            Destroy(this);
        }
    }


    private IEnumerator<float> _CameraLookAtTarget()
    {
        // Show player
        var playerTransform = GameManager.Instance.player.transform;
        playerTransform.gameObject.SetActive(true);

        // Animate
        var t = 0.0f;
        var lookDirection = mainCamera.transform.forward;
        var currentLook = gameCamera.GetLookRot();
        var currentPivot = gameCamera.GetPivotRot();

        while (t < animationTime)
        {
            t += Time.deltaTime;
            var ratio = t / animationTime;
            
            // Camera look
            var newRot = Quaternion.Lerp(currentLook, Quaternion.Euler(targetLookRot), ratio);
            var newPivotRot = Quaternion.Lerp(currentPivot, Quaternion.Euler(targetPivotAngles), ratio);
            gameCamera.SetLook(newRot, newPivotRot);
            gameCamera.ApplyRotation();
            playerTransform.LookAt(mainCamera.transform.forward);
            yield return 0;
        }
        talkEvent.OnDrag(playerTransform, false);
        eventIndex = 3;
    }
}
