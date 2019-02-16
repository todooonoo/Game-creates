using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    [SerializeField] protected string tutorialName;
    [SerializeField] protected GameObject tutorialObject;
    [SerializeField] protected Tutorial nextTutorial;
    protected static Tutorial currentTutorial;
    protected bool tutorialActive;

    public virtual void BeginTutorial()
    {
        tutorialActive = true;
        if (SaveManager.EventCleared(tutorialName))
        {
            tutorialActive = false;
            EndTutorial(false);
            return;
        }

        currentTutorial = this;

        TutorialManager.Instance.BeginTutorial(tutorialObject);
        SaveManager.AddEventClear(tutorialName);
    }

    protected virtual void EndTutorial(bool saveClear = true)
    {
        if (nextTutorial)
            nextTutorial.BeginTutorial();
        Destroy(gameObject);
    }
}
