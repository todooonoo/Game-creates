using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private static float drawRadius = 0.25f;

    public Vector3 RespawnPos { get { return transform.position + delta; } }

    [SerializeField]
    private Vector3 delta;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1.0f, 0.5f);
        Gizmos.DrawSphere(transform.position, drawRadius);
    }
}
