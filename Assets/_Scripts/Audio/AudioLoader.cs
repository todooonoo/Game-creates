using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoader : MonoBehaviour {

    [SerializeField]
    private AudioPair audioPair;
    [SerializeField]
    private bool startBgm;

	void Start ()
    {
        AudioManager.Instance.LoadSound(audioPair);

        if (startBgm)
            AudioManager.Instance.Play(audioPair.audioID);
	}
}
