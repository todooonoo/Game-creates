using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorldAxis
{
    X,
    Y,
    Z,
    XZ
}

public class WorldObjectCloneContact : MonoBehaviour {

    [SerializeField] private WorldAxis axis;
    private WorldObjectClone clone;

    private void Start()
    {
        clone = GetComponentInParent<WorldObjectClone>();
    }
    
    public void SetContactPos(Player player)
    {
        var managerClone = player.GetComponentInParent<WorldObjectClone>();

        if(managerClone)
        {
            managerClone.SetOriginPosition(WorldManager.Instance.currentWorld);

            var managerOrigin = managerClone.origin;
            var playerOrigin = managerOrigin.GetComponentInChildren<Player>();

            if(playerOrigin)
            {
                var pos = playerOrigin.transform.position;
                switch(axis)
                {
                    case WorldAxis.X:
                        pos.x = clone.origin.transform.position.x;
                        break;
                    case WorldAxis.Y:
                        pos.y = clone.origin.transform.position.y;
                        break;
                    case WorldAxis.Z:
                        pos.z = clone.origin.transform.position.z;
                        break;
                    case WorldAxis.XZ:
                        pos.x = clone.origin.transform.position.x;
                        pos.z = clone.origin.transform.position.z;
                        break;
                }
                Debug.Log(transform.parent.name);
                playerOrigin.transform.position = pos;
            }
        }
    } 
}
