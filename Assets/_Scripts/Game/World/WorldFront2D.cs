﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldFront2D : World {

    public override Vector3 GetCurrentWorldPos(Vector3 pos)
    {
        return pos;
    }

    public override Vector3 GetOriginWorldPos(Vector3 currentPos, Vector3 originPos)
    {
        return currentPos;
    }
}