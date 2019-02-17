using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMStopper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.StopBGM();
    }
}
