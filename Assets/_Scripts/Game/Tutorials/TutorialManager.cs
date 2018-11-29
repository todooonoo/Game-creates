using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;

public class TutorialManager : Singleton<TutorialManager> {

    [SerializeField] private Transform background;
    [SerializeField] private float animationTime = 1.0f;
    private bool animating;
    private GameObject currentTutorialObject;

    public void BeginTutorial(GameObject tutorialObject)
    {
        if (currentTutorialObject)
            currentTutorialObject.Recycle();
        currentTutorialObject = tutorialObject.Spawn(background);
        currentTutorialObject.transform.localPosition = Vector3.zero;
        Timing.RunCoroutine(_ShowTutorial());
    }

    private IEnumerator<float> _ShowTutorial()
    {
        if (background.gameObject.activeSelf)
            yield return Timing.WaitUntilDone(Timing.RunCoroutine(_HideTutorial()));
        background.gameObject.SetActive(true);

        var t = 0.0f;
        var scale = new Vector3(0, 1, 1);
        while(t < animationTime)
        {
            t += Time.deltaTime;
            scale.x = Mathf.Clamp(t / animationTime, 0, 1);
            background.transform.localScale = scale;
            yield return 0;
        }
    }

    private IEnumerator<float> _HideTutorial()
    {
        var t = 0.0f;
        var scale = new Vector3(1, 1, 1);
        while (t < animationTime)
        {
            t += Time.deltaTime;
            scale.x = Mathf.Clamp(1.0f - t / animationTime, 0, 1);
            background.transform.localScale = scale;
            yield return 0;
        }
        background.gameObject.SetActive(false);
    }
}
