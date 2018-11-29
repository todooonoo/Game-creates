using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTutorial : Tutorial {

    protected bool activated;

    public override void BeginTutorial()
    {
        base.BeginTutorial();
        activated = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activated)
            return;

        var player = other.GetComponent<Player>();

        if (player)
            BeginTutorial();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated)
            return;

        var player = other.GetComponent<Player>();

        if (player)
            BeginTutorial();
    }
}
