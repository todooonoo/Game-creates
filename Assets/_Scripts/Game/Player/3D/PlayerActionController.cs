using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : PlayerComponent {

    public override void HandleUpdate(Player3D player)
    {
        if (player.playerState == PlayerState.Action)
            return;

        var input = InputHandler.Instance.GetInput(InputAction.Attack);

        if(input.GetAxisDown)
        {
            player.AnimationController.TriggerAttack();
            player.playerState = PlayerState.Action;
        }
    }
}
