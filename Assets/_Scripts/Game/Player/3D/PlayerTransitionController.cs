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
            TransitionScreen.Instance.SetVisible(true);
            player.playerState = PlayerState.Transition;
        } else if(transitionInput.GetAxisUp)
        {
            ResetState(player);
        }
    }

    public void ResetState()
    {
        ResetState(GetComponent<Player3D>());
    }

    public void ResetState(Player3D player)
    {
        TransitionScreen.Instance.SetVisible(false);
        GameManager.Instance.LockCursor(true);
        player.playerState = PlayerState.Idle;
    }
}
