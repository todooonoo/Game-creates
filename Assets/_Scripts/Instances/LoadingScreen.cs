﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : Singleton<LoadingScreen> {
    
    [SerializeField]
    private Image background;
    [SerializeField]
    private float animationTime = 0.5f;
    private const float frameTime = 0.04f;

    [SerializeField]
    private bool enableReload = true;

    public bool IsLoading { get; private set; }
    public string CurrentScene { get; private set; }

    protected override void Init()
    {
        CurrentScene = SceneManager.GetActiveScene().name;

        // Hide background on load
        background.SetAlpha(0.0f);
        background.gameObject.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
		if (IsLoading)
			return;
        StartCoroutine(AnimateLoad(sceneName));
    }

    private void Update()
    {
        if (enableReload && Input.GetKeyDown(KeyCode.F5))
            ReloadScene();
    }

    private IEnumerator AnimateLoad(string sceneName)
    {
		IsLoading = true;
        CurrentScene = sceneName;

        // Animate fade-in
        yield return AnimateBackgroundFade(1.0f);

        // Pause time
        Time.timeScale = 0f;

        // Load scene
        var loadOperation = SceneManager.LoadSceneAsync(sceneName);
        while (!loadOperation.isDone)
            yield return new WaitForSecondsRealtime(frameTime);

        Time.timeScale = 1f;

        // Animate fade-out
        yield return AnimateBackgroundFade(0.0f);
		IsLoading = false;
    }

    public IEnumerator AnimateBackgroundFade(float target)
    {
        background.gameObject.SetActive(true);

        var t = 0.0f;
        var currentAlpha = background.color.a;
        while(t < animationTime)
        {
            t += frameTime;
            background.SetAlpha(Mathf.Lerp(currentAlpha, target, t / animationTime));
            yield return new WaitForSecondsRealtime(frameTime);
        }
        background.gameObject.SetActive(target > 0);
	}

	public void ReloadScene()
	{
		LoadScene(SceneManager.GetActiveScene().name);
	}
}
