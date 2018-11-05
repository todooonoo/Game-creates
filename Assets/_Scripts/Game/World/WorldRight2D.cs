using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRight2D : World {

    public override Vector3 GetCurrentWorldPos(Vector3 pos)
    {
        return new Vector3(pos.z, pos.y, 0);
    }

    public override Vector3 GetOriginWorldPos(Vector3 currentPos, Vector3 originPos)
    {
        return new Vector3(originPos.x, currentPos.y, currentPos.x);
    }
}
