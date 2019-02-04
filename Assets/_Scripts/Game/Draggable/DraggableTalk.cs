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

    public override bool OnDrag(Transform parent)
    {
        if(clearType == TalkClearType.UnlockSide)
        {
            GameManager.Instance.transitionUnlocked = true;
        }

        GameManager.Instance.dialogueWindow.SetDialogue(lines);

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
