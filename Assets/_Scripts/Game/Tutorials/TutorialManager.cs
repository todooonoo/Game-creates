using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;

public class TutorialManager : Singleton<TutorialManager> {

    [SerializeField] private RectTransform background;
    [SerializeField] private float animationTime = 1.0f;
    private bool animating;
    private GameObject currentTutorialObject;
    private float height, showY, hideY;

    protected override void Init()
    {
        base.Init();
        height = background.rect.height;
        showY = background.localPosition.y;
        hideY = background.localPosition.y + height;
        background.localPosition = new Vector3(background.localPosition.x, hideY);
        background.gameObject.SetActive(false);
    }

    public void BeginTutorial(GameObject tutorialObject)
    {
        Timing.RunCoroutine(_ShowTutorial(tutorialObject));
    }

    private void SetNewTutorial(GameObject tutorialObject)
    {
        var temp = tutorialObject.Spawn(background);
        if (currentTutorialObject)
        {
            temp.transform.localPosition = currentTutorialObject.transform.localPosition;
            currentTutorialObject.Recycle();
        }
        currentTutorialObject = temp;
    }


    private IEnumerator<float> _ShowTutorial(GameObject tutorialObject)
    {
        // Hide previous tutorial
        if (background.gameObject.activeSelf)
        {
            yield return Timing.WaitUntilDone(Timing.RunCoroutine(_HideTutorial()));
        }

        // Show new tutorial
        SetNewTutorial(tutorialObject);
        background.gameObject.SetActive(true);

        var t = 0.0f;
        while(t < animationTime)
        {
            t += Time.deltaTime;
            background.transform.localPosition = 
                new Vector3(background.localPosition.x, Mathf.Lerp(hideY, showY, t / animationTime));
            yield return 0;
        }
    }
    private IEnumerator<float> _HideTutorial()
    {
        var t = 0.0f;
        while (t < animationTime)
        {
            t += Time.deltaTime;
            background.transform.localPosition =
                new Vector3(background.localPosition.x, Mathf.Lerp(showY, hideY, t / animationTime));
            yield return 0;
        }
        background.gameObject.SetActive(false);
    }
}
