using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTriggerStart : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return null;
        Destroy(GetComponent<LevelTrigger>());
    }
}
