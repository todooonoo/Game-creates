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
        if (player.playerState == PlayerState.Action || (!GameManager.Instance.transitionUnlocked && !Application.isEditor))
            return;
        
        if (transitionInput.GetAxisDown)
        {
            GameManager.Instance.transitionScreen.SetVisible(true);
            player.playerState = PlayerState.Transition;
        } else if(transitionInput.GetAxisUp)
        {
            GameManager.Instance.transitionScreen.SetVisible(false);
            GameManager.Instance.LockCursor(true);
            player.playerState = PlayerState.Idle;
        }
    }
}
