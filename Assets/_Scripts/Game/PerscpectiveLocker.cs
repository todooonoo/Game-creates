using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerscpectiveLocker : MonoBehaviour
{
    public static PerscpectiveLocker Instance { get; private set; }

    public bool sideLocked, frontLocked, topLocked;

    private void Awake()
    {
        Instance = this;
    }
}
