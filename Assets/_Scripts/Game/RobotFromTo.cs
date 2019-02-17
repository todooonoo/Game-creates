using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFromTo : MonoBehaviour
{
    public Vector3 localDelta;
    public float waitTime = 1.0f, speed = 1.0f;

    private Vector3 startPos, targetPos, dir;
    private float time;
    private bool waiting;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + localDelta;
        ResetDir();
    }

    private void ResetDir()
    {
        dir = Vector3.Normalize(targetPos - startPos);
        transform.LookAt(transform.position + dir);
        waiting = false;
    }
    
    private void Update()
    {
        HandleUpdate();
    }

    public void HandleUpdate()
    {
        if(waiting)
        {
            time += Time.deltaTime;
            if(time >= waitTime)
            {
                time = 0.0f;
                Vector3 temp = targetPos;
                targetPos = startPos;
                startPos = temp;
                ResetDir();
            }
            return;
        }

        float distSqr = Vector3.SqrMagnitude(targetPos - transform.position);
        float d = speed * Time.deltaTime;

        if (distSqr > d * d)
        {
            transform.position += dir * d;
        } else
        {
            transform.position = targetPos;
            waiting = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow * 0.5f;
        Gizmos.DrawSphere(transform.position + localDelta, 0.25f);
    }
}
