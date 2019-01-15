using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransitionController : PlayerComponent {
    
    private InputPair transitionInput;

    private void Start()
    {
        transitionInput = InputHandler.Instance.GetInput(InputAction.TransitionMain);
    }

    public override void HandleUpdate(Player3D player)
    {
        if (player.playerState == PlayerState.Action)
            return;
        
        if (transitionInput.GetAxisDown)
        {
            TransitionScreen.Instance.SetVisible(true);
            player.playerState = PlayerState.Transition;
        } else if(transitionInput.GetAxisUp)
        {
            TransitionScreen.Instance.SetVisible(false);
            GameManager.Instance.LockCursor(true);
            player.playerState = PlayerState.Idle;
        }
    }
}
