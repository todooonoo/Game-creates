using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private static float drawRadius = 0.25f;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1.0f, 0.5f);
        Gizmos.DrawSphere(transform.position, drawRadius);
    }
}
