using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinableDeath : Combinable
{
    public override void HandleUpdate(Player2D player)
    {
        DeathScreen.Instance.Show();
    }

    public override bool SetPos()
    {
        return false;
    }
}
