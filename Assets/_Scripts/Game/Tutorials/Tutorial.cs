using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    [SerializeField] protected GameObject tutorialObject;
    [SerializeField] protected Tutorial nextTutorial;

    public virtual void BeginTutorial()
    {
        TutorialManager.Instance.BeginTutorial(tutorialObject);
    }

    protected virtual void EndTutorial()
    {
        if (nextTutorial)
            nextTutorial.BeginTutorial();
        Destroy(gameObject);
    }
}
