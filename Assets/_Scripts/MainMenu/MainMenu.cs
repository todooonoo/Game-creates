using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class MainMenu : MonoBehaviour {
    
    [Header("Start Fade in")]
    [SerializeField] private GameObject[] hideObjects;
    [SerializeField] private float startDelay = 3.0f;

    [Header("Animation Vars")]
    [SerializeField] private Transform skyboxTransform;
    [SerializeField] private Transform sunTransform;
    [SerializeField] private float rotationTime;
    [SerializeField] private Light sceneLight;
    [SerializeField] private float minLightIntensity;
    [SerializeField] private float maxLightIntensity;
    [SerializeField] private Color minLightColor;
    [SerializeField] private Color maxLightColor;
    [SerializeField] private LensFlare flare;
    private float currentTime;

    [Header("Game Start")]
    [SerializeField] private string startSceneName;
    private bool initComplete;

	void Start ()
    {
		for(int i = 0; i < hideObjects.Length; i++)
        {
            hideObjects[i].SetActive(false);
        }
        BlackScreen.Instance.SetAlpha(1.0f);
        Timing.RunCoroutine(_Init());
	}

    private IEnumerator<float> _Init()
    {
        // Animate fade in
        float t = 0.0f;
        while(t < startDelay)
        {
            t += Time.deltaTime;
            BlackScreen.Instance.SetAlpha(1.0f - t / startDelay);
            yield return 0;
        }

        // Show hidden objects
        for (int i = 0; i < hideObjects.Length; i++)
        {
            hideObjects[i].SetActive(true);
        }
        initComplete = true;
    }
	
	void Update ()
    {
        Animate(Time.deltaTime);

        if (!initComplete)
            return;

        CheckInput();
	}

    private void Animate(float time)
    {
        // Update time
        currentTime += time;
        if (currentTime > rotationTime)
            currentTime -= rotationTime;

        // Rotate skybox etc..
        /*
        Vector3 rotation = Vector3.up * 360 * time / rotationTime;
        skyboxTransform.Rotate(rotation);
        sunTransform.Rotate(-rotation);

        // Animate light color change
        if (currentTime < rotationTime / 2)
        {
            float t = currentTime * 2 / rotationTime;
            sceneLight.intensity = 
                Mathf.Lerp(maxLightIntensity, minLightIntensity, t);
            sceneLight.color =
                Color.Lerp(maxLightColor, minLightColor, t);
        } else
        {
            float t = 2 * currentTime / rotationTime - 1;
            sceneLight.intensity =
                Mathf.Lerp(minLightIntensity, maxLightIntensity, t);
            sceneLight.color =
                Color.Lerp(minLightColor, maxLightColor, t);
        }
        */
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            SaveManager.Instance.RestartSave();
            LoadingScreen.Instance.LoadScene(startSceneName);
            enabled = false;
        }
    }
}
