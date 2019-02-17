using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProgress : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ProgressManager.Instance.transitionFrontUnlocked = false;
        ProgressManager.Instance.transitionUnlocked = false;
        ProgressManager.Instance.transitionUpUnlocked = false;
        ProgressManager.Instance.doorsLocked = false;
        SaveManager.Instance.RestartSave();
        TutorialManager.Instance.HideTutorial();
    }
}
