using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUp2D : World {

    public float raycastHeight = -1;

    public override Vector3 GetCurrentWorldPos(Vector3 pos)
    {
        return new Vector3(pos.x, pos.z, 0);
    }

    public override Vector3 GetOriginWorldPos(Vector3 currentPos, Vector3 originPos)
    {
        Vector3 pos = new Vector3(currentPos.x, originPos.y, currentPos.y);
        if (raycastHeight > 0)
        {
            World world3D = WorldManager.Instance.GetWorld(WorldType.World3D);
            world3D.gameObject.SetActive(true);

            RaycastHit hit;
            Vector3 raycastPos = new Vector3(currentPos.x, raycastHeight, currentPos.y);
            if(Physics.Raycast(raycastPos, Vector3.down, out hit, raycastHeight))
            {
                pos.y = hit.point.y;
            }
            world3D.gameObject.SetActive(false);
        }
        return pos;
    }
}
