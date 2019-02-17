using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableTerminal : Draggable
{
    public string sceneName;

    public override bool OnDrag(Transform parent, bool reverse = false)
    {
        LoadingScreen.Instance.LoadScene(sceneName);
        return false;
    }
}
