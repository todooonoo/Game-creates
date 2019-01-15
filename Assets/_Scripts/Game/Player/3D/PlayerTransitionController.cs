using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransitionController : PlayerComponent {

    [SerializeField]
    private GameObject transitionUIObject;

    private InputPair transitionInput;

    private void Start()
    {
        transitionInput = InputHandler.Instance.GetInput(InputAction.TransitionMain);
    }

    public override void HandleUpdate(Player3D player)
    {
        if (player.playerState == PlayerState.Idle)
            return;

        if(transitionUIObject)
            transitionUIObject.SetActive(transitionInput.GetAxis);
    }
}
