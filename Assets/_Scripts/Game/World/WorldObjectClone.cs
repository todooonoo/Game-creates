using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObjectClone : MonoBehaviour {

    [HideInInspector]
    public WorldObject origin;
    public bool isManager;

    protected virtual void Start()
    {
        if (isManager)
            transform.SetAsLastSibling();
    }

    public void SetCurrentPosition(World world)
    {
        if(isManager)
        {
            var originPlayer = origin.GetComponentInChildren<Player>();
            var player = GetComponentInChildren<Player>();

            if(player && originPlayer)
            {
                player.transform.position = world.GetCurrentWorldPos(originPlayer.transform.position);
            }
            var gameCamera = GetComponentInChildren<GameCamera>();
            if (gameCamera)
                gameCamera.GoToTarget();

            return;
        }
        transform.position = world.GetCurrentWorldPos(origin.transform.position);
    }

    public void SetOriginPosition(World world)
    {
        if (isManager)
        {
            var originPlayer = origin.GetComponentInChildren<Player>();
            var player = GetComponentInChildren<Player>();

            if (player && originPlayer)
            {
                originPlayer.transform.position = world.GetOriginWorldPos(player.transform.position,
                    originPlayer.transform.position);
            }

            var gameCamera = originPlayer.transform.parent.GetComponentInChildren<GameCamera>();
            if (gameCamera)
                gameCamera.GoToTarget();
            return;
        }
        origin.transform.position = world.GetOriginWorldPos(transform.position, origin.transform.position);
    }
}
