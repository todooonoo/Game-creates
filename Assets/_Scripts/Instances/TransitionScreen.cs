using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScreen : Singleton<TransitionScreen>
{
    public GameObject screenObject;
    private const string transitionSFXName = "Transition";

    public void SetVisible(bool visible)
    {
        screenObject.SetActive(visible);
    }

    public void Transition(int worldType)
    {
        SetVisible(false);
        WorldManager.Instance.Transition((WorldType)worldType);
        AudioManager.Instance.PlaySFX(transitionSFXName);
    }

    public void CameraPreview()
    {

    }
}
