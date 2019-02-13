using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow2D : MonoBehaviour
{
    public float multiplier = 1.0f;
    public bool xAxisOnly = true;
    private Vector3 oldPosition;

    private void OnEnable()
    {
        oldPosition = GameManager.Instance.player.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 newPosition = GameManager.Instance.player.transform.position;
        Vector3 delta = newPosition - oldPosition;
        transform.position += new Vector3(delta.x, xAxisOnly ? 0 : delta.y) * multiplier;
        oldPosition = newPosition;
    }
}
