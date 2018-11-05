using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera2D : GameCamera
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

    public override void HandleUpdate()
    {
        // All logic in FollowTarget() ?
    }

    protected override void FollowTarget(float deltaTime)
    {
        var targetPos = m_Target.position + offset;
        var smoothPos = Vector3.Lerp(transform.position, targetPos, speed * deltaTime);
        transform.position = smoothPos;
    }
}
