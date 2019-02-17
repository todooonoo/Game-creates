using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinableDeath : Combinable
{
    private bool done;

    public override void HandleUpdate(Player2D player)
    {
        if (done)
            return;
        DeathScreen.Instance.Show();
    }

    public override bool SetPos()
    {
        return false;
    }
}
