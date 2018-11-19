using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PPSetter : MonoBehaviour {

    [SerializeField] private PostProcessingProfile profile;
    [SerializeField] private bool moveManagerHere = true;

    // Use this for initialization
    void Start () {
        GameManager.Instance.transform.position = transform.position;
        GameManager.Instance.Camera.GetComponent<PostProcessingBehaviour>().profile = profile;
	}
}
