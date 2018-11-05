using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationPerSecond;
    
    private void FixedUpdate()
    {
        transform.Rotate(rotationPerSecond * Time.fixedDeltaTime);
    }
}
