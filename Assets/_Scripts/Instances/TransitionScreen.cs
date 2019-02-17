using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScreen : Singleton<TransitionScreen>
{
    public GameObject screenObject, upTransitionButton, frontTransitionButton, sideTransitionButton;
    private const string transitionSFXName = "Transition";

    public InteractIcon interactIcon;
    public DialogueWindow dialogueWindow;

    protected override void Init()
    {
        base.Init();

        if (dialogueWindow)
            dialogueWindow.InitUI();
    }

    public void SetVisible(bool visible)
    {
        screenObject.SetActive(visible);
        upTransitionButton.SetActive(ProgressManager.Instance.transitionUpUnlocked || Application.isEditor);
        frontTransitionButton.SetActive(ProgressManager.Instance.transitionFrontUnlocked || Application.isEditor);

        if(visible)
        {
            sideTransitionButton.GetComponent<Button>().interactable 
                = (!PerscpectiveLocker.Instance || !PerscpectiveLocker.Instance.sideLocked);
            upTransitionButton.GetComponent<Button>().interactable
                = (!PerscpectiveLocker.Instance || !PerscpectiveLocker.Instance.topLocked);
            frontTransitionButton.GetComponent<Button>().interactable
                = (!PerscpectiveLocker.Instance || !PerscpectiveLocker.Instance.frontLocked);
        }
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
