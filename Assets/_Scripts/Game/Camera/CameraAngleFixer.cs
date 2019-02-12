using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAngleFixer : MonoBehaviour
{
    [SerializeField]
    private float angleMin, angleMax;
    private GameCamera3D gameCamera;
    
    private void Start()
    {
        gameCamera = GameManager.Instance.gameCamera.GetComponent<GameCamera3D>();
    }

    private void LateUpdate()
    {
        gameCamera.m_TiltMin = angleMin;
        gameCamera.m_TiltMax = angleMax;
    }
}
