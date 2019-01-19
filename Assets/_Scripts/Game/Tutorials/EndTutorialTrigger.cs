using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTutorialTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player)
            TutorialManager.Instance.HideTutorial();
    }

}
