using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableTalk : Draggable
{
    public string[] lines;

    public override bool OnDrag(Transform parent)
    {
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
