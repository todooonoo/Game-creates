using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSave
{
    public PlayerSave playerSave;

    public GameSave()
    {
        playerSave = new PlayerSave();
    }
}
