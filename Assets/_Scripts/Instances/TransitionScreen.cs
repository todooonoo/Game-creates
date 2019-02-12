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
        upTransitionButton.SetActive(ProgressManager.Instance.transitionUpUnlocked || Application.isEditor);
        frontTransitionButton.SetActive(ProgressManager.Instance.transitionFrontUnlocked || Application.isEditor);
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
