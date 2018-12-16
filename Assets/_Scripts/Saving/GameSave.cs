using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSave
{
    public List<string> clearedEvents;

    public GameSave()
    {
        clearedEvents = new List<string>();
    }
}
