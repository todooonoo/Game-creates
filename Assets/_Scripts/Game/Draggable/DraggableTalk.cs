using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TalkClearType
{
    None,
    UnlockSide,
    UnlockUp,
    UnlockFront
}

public class DraggableTalk : Draggable
{
    public string[] lines;
    public TalkClearType clearType;

    public override bool OnDrag(Transform parent, bool reverse)
    {
        if (reverse)
            return false;

        if(clearType == TalkClearType.UnlockSide)
        {
            ProgressManager.Instance.transitionUnlocked = true;
        }

        TransitionScreen.Instance.dialogueWindow.SetDialogue(lines);

        DialogueChanger changer = GetComponent<DialogueChanger>();
        if(changer)
        {
            lines = changer.newLines;
        }
        return false;
    }

    public override void OnRelease(Transform parent)
    {
        // Do nothing
    }
}
