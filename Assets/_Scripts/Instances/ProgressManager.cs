﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : Singleton<ProgressManager>
{
    [HideInInspector]
    public bool transitionUnlocked, transitionUpUnlocked;
}
