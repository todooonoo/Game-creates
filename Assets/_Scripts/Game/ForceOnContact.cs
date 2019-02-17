using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceOnContact : MonoBehaviour
{
    public Vector3 force;
    private static string bumpSFXName = "Bump";


    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rBody = collision.collider.attachedRigidbody;

        if(rBody)
        {
            rBody.AddForce(force, ForceMode.VelocityChange);
            AudioManager.Instance.PlaySFX(bumpSFXName);
        }
    }
}
