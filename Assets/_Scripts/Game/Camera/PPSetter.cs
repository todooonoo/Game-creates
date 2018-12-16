using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

[ExecuteInEditMode]
public class PPSetter : MonoBehaviour {

    [SerializeField] private PostProcessingProfile profile;
    
    private void Update()
    {
        if (profile)
        {
            PostProcessingBehaviour behavior = FindObjectOfType<PostProcessingBehaviour>();

            if (behavior)
            {
                behavior.profile = profile;

                if (!Application.isEditor)
                    enabled = false;
            }
        }
    }
}
