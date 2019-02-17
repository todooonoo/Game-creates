using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Laser beam force back
public class ForceOnContact : MonoBehaviour
{
    public Vector3 force;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rBody = collision.collider.attachedRigidbody;

        if(rBody)
        {
            /*
            // Brute force laser check
            if (rBody.transform.position.z < transform.position.z)
                rBody.AddForce(force, ForceMode.VelocityChange);
            else
                rBody.AddForce(-force, ForceMode.VelocityChange);
                */
            DeathScreen.Instance.Show();
        }
    }
}
