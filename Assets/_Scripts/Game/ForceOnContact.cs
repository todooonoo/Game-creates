using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Laser beam force back
public class ForceOnContact : MonoBehaviour
{
    public Vector3 force;
    private float time;
    private static float waitTime = 0.1f;

    private void OnEnable()
    {
        time = 0;
    }
    
    private void Update()
    {
        if(time < waitTime)
            time += Time.deltaTime;
    }

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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (time < waitTime)
            return;
        
        Rigidbody2D rBody = collision.collider.attachedRigidbody;

        if (rBody)
        {
            var player2D = rBody.GetComponent<Player2D>();

            if (player2D && !player2D.combinable)
            {
                DeathScreen.Instance.Show();
            }
        }
    }
}
