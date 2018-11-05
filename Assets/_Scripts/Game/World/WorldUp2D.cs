using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUp2D : World {

    public override Vector3 GetCurrentWorldPos(Vector3 pos)
    {
        return new Vector3(pos.x, pos.z, 0);
    }

    public override Vector3 GetOriginWorldPos(Vector3 currentPos, Vector3 originPos)
    {
        return new Vector3(currentPos.x, originPos.y, currentPos.y);
    }
}
