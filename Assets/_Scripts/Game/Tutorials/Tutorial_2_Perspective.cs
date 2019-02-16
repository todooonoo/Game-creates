using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_2_Perspective : Tutorial
{
    public Transform lookTarget;
    public float animationTime = 1.0f;
    public DraggableTalk talkEvent;

    private GameCamera gameCamera;
    private Camera mainCamera;

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
    }
}
