using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSave
{
    public List<string> clearedEvents;
    public bool powerUnlocked;

    public GameSave()
    {
        clearedEvents = new List<string>();
    }
}
