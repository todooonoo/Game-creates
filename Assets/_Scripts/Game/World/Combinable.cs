using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combinable : MonoBehaviour
{
    public string[] linesMove = new string[] { "Unable to move.\n (Combined with an immovable object).",
    "Press TAB to revert dimensions." };
    [HideInInspector]
    public bool forceTransition;
    private Transform originalParent;

    private static readonly string bumpSfxName = "Bump";

    public void CheckParent()
    {
        if (!originalParent)
            originalParent = transform.parent;
    }

    public void ResetParent()
    {
        transform.parent = originalParent;
    }

    public virtual bool SetPos()
    {
        return true;
    }

    public virtual void HandleUpdate(Player2D player)
    {
        if(linesMove.Length > 0)
        {
            var moveDelta = new Vector2(Input.GetAxis(Static.horizontalAxis), Input.GetAxis(Static.verticalAxis));

            if (moveDelta.x != 0 || moveDelta.y != 0)
            {
                TransitionScreen.Instance.dialogueWindow.SetDialogue(linesMove);
                AudioManager.Instance.PlaySFX(bumpSfxName);
            }
            else
            {
                player.CheckTransition(true);
            }
            return;
        }
        player.Locomotion();
    }
}
