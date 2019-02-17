using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinableHelpRobot : Combinable
{
    public bool ignoreYAxis = true;
    private HelpRobot2D robotScript;
    private InputPair transitionInput;
    private const string transitionSFXName = "Transition";

    private void Start()
    {
        forceTransition = true;
        robotScript = GetComponentInParent<HelpRobot2D>();
        transitionInput = InputHandler.Instance.GetInput(InputAction.TransitionMain);
    }

    public override void HandleUpdate(Player2D player)
    {
        robotScript.HandleUpdate();
        player.SetPosCombinableCenter(ignoreYAxis);

        if(transitionInput.GetAxisDown)
        {
            WorldManager.Instance.Transition(WorldType.World3D);
            AudioManager.Instance.PlaySFX(transitionSFXName);
        }
    }
}
