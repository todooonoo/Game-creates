using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScreen : Singleton<TransitionScreen>
{
    public GameObject screenObject, upTransitionButton, frontTransitionButton;
    private const string transitionSFXName = "Transition";

    public void SetVisible(bool visible)
    {
        screenObject.SetActive(visible);
        upTransitionButton.SetActive(ProgressManager.Instance.transitionUpUnlocked);
        frontTransitionButton.SetActive(ProgressManager.Instance.transitionFrontUnlocked);
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
