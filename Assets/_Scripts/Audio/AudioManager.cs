using UnityEngine;
using System;
using System.Collections;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private AudioRange[] audioRanges;

    [SerializeField]
    private float bgmFadeTime = 2.0f;

    // Stop bgmRange vari
    private int bgmRangeIndex;
    private AudioSource bgmSource;

    protected override void Awake()
    {
        base.Awake();

        if (Instance != this)
            return;

        // Load BGM source
        for (int i = 0; i < audioRanges.Length; i++)
        {
            if (audioRanges[i].audioType == AudioType.BGM)
            {
                bgmRangeIndex = i;
                bgmSource = audioRanges[i].prefab.Spawn(transform);
                break;
            }
        }
    }

    public void LoadSound(AudioPair audioPair)
    {
        // Loop over audio ranges
        for(int i = 0; i < audioRanges.Length; i++)
        {
            var range = audioRanges[i];
            
            // Range found
            if(range.ContainsID(audioPair.GetID))
            {
                // Check if clip already loaded
                var count = range.pairs.Count;
                var clipLoaded = false;

                for(int j = 0; j < count; j++)
                {
                    if(range.pairs[j] == audioPair)
                    {
                        clipLoaded = true;
                        break;
                    }
                }

                // Add clip to list
                if(!clipLoaded)
                    range.pairs.Add(audioPair);
                break;
            }
        }
    }

    public void Play(AudioID id, AudioSource source = null)
    {
        // Loop over audio ranges
        for (int i = 0; i < audioRanges.Length; i++)
        {
            var range = audioRanges[i];

            // Range found
            if (range.ContainsID(id))
            {
                var count = range.pairs.Count;
                for (int j = 0; j < count; j++)
                {
                    var pair = range.pairs[j];
                    if (pair.audioID == id)
                    {
                        // Play clip
                        PlayClip(range, pair.clip, source);
                        return;
                    }
                }
                return;
            }
        }
    }

    private void PlayClip(AudioRange range, AudioClip clip, AudioSource source)
    {
        // If audio source defined play it there
        if(source)
        {
            source.clip = clip;
            source.Play();
            return;
        }

        if (range.audioType == AudioType.BGM)
        {
            StartCoroutine(PlayBGM(clip));
        }
        else
        {
            StartCoroutine(PlaySFXEnum(range.prefab));
        }
    }

    private IEnumerator PlaySFXEnum(AudioSource sfxPrefab)
    {
        var sfxInstance = sfxPrefab.Spawn();
        sfxInstance.Play();

        while (sfxInstance && sfxInstance.isPlaying)
            yield return null;

        if (sfxInstance)
            sfxInstance.Recycle();
    }

    private IEnumerator PlayBGM(AudioClip clip)
    {
        if(bgmSource.clip)
        {
            yield return ChangeVolume(bgmSource, 0.0f, bgmFadeTime);
            bgmSource.Stop();
        }
        bgmSource.clip = clip;
        bgmSource.Play();
        yield return ChangeVolume(bgmSource, audioRanges[bgmRangeIndex].volume, bgmFadeTime);
    }

    private IEnumerator ChangeVolume(AudioSource source, float targetVolume, float time)
    {
        var t = 0.0f;
        var startVolume = source.volume;

        while(t < time)
        {
            t += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, targetVolume, t / time);
            yield return null;
        }
    }
}
