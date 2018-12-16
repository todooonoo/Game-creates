using UnityEngine;
using System;
using System.Collections;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private float bgmChangeTime = 3.0f;

    [SerializeField]
    private AudioSource[] bgmPrefabs;
    private AudioSource currentSource;
    private bool settingBGM;

    public void SetBGM(AudioSource bgmPrefab, bool instant = false)
    {
        StartCoroutine(SetBGMEnum(bgmPrefab, instant));
    }

    public void SetBGM(string name, bool instant = false)
    {
        for (int i = 0; i < bgmPrefabs.Length; i++)
        {
            if (bgmPrefabs[i].name == name)
            {
                SetBGM(bgmPrefabs[i], instant);
                break;
            }
        }
    }

    public void StopBGM(bool instant = false)
    {
        StartCoroutine(StopCurrentBGM(instant));
    }

    private IEnumerator SetBGMEnum(AudioSource bgmObject, bool instant = false)
    {
        bgmObject.transform.SetParent(transform);

        while (settingBGM)
            yield return null;

        // Check same bgm
        if (currentSource && bgmObject && currentSource.name == bgmObject.name)
        {
            Debug.Log("BGM: Same name, ignoring...");
            yield break;
        }

        settingBGM = true;
        yield return StopCurrentBGM(instant);
        
        currentSource = bgmObject;
        currentSource.Play();

        if (!instant)
            yield return FadeInCurrentBGM();
        settingBGM = false;
    }

    private IEnumerator StopCurrentBGM(bool instant)
    {
        if (currentSource)
        {
            if (!instant)
            {
                while (currentSource.volume > 0.0f)
                {
                    currentSource.volume -= Time.fixedDeltaTime / bgmChangeTime;
                    yield return new WaitForFixedUpdate();
                }
                currentSource.volume = 0.0f;
            }
            Destroy(currentSource);
        }
    }

    private IEnumerator FadeInCurrentBGM()
    {
        var targetVolume = currentSource.volume;
        currentSource.volume = 0.0f;

        while (currentSource.volume < targetVolume)
        {
            currentSource.volume = Mathf.Min(targetVolume, currentSource.volume + Time.fixedDeltaTime / bgmChangeTime);
            yield return new WaitForFixedUpdate();
        }
        currentSource.volume = targetVolume;
    }
}
