using System;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : Singleton<InputHandler> {

    [SerializeField]
    private MouseInputPair[] mousePairs;
    [SerializeField]
    private KeyInputPair[] keyPairs;

    private void Update()
    {
        for (int i = 0; i < mousePairs.Length; i++)
            mousePairs[i].HandleUpdate();
        for (int i = 0; i < keyPairs.Length; i++)
            keyPairs[i].HandleUpdate();
    }

    public InputPair GetInput(InputAction action)
    {
        for (int i = 0; i < mousePairs.Length; i++)
        {
            if (mousePairs[i].action == action)
                return mousePairs[i];
        }
        for (int i = 0; i < keyPairs.Length; i++)
        {
            if (keyPairs[i].action == action)
                return keyPairs[i];
        }
        return null;
    }
}
