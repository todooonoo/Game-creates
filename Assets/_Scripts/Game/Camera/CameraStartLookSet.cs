using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStartLookSet : MonoBehaviour
{
    public GameCamera3D gameCamera;
    public Vector3 rot, pivotRot;

    // Start is called before the first frame update
    void Start()
    {
        gameCamera.SetLook(Quaternion.Euler(rot), Quaternion.Euler(pivotRot));
    }
}
