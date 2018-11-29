using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    [SerializeField] protected GameObject tutorialObject;
    [SerializeField] protected Tutorial removeTutorial;

	public virtual void BeginTutorial()
    {
        if (removeTutorial)
            Destroy(removeTutorial.gameObject);
        TutorialManager.Instance.BeginTutorial(tutorialObject);
    }
}
