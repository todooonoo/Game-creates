using System;
using System.Collections.Generic;
using UnityEngine;


public enum AudioType
{
    BGM,
    SFX,
    Voice
}

public enum AudioID
{
    // BGM (0 - 199)
    TestBGM = 199,

    // SFX (200 - 499)

    // Voice (500 - 999)
}

[Serializable]
public struct AudioRange
{
    public AudioType audioType;     // The type of audio (BGM, SFX, etc...)
    public int minID, maxID;        // Min-Max audio id range
    public float volume;     // The default volume of the sound
    public List<AudioPair> pairs;   // List of clips loaded
    public AudioSource prefab;

    public bool ContainsID(int id)
    {
        return id >= minID && id <= maxID;
    }

    public bool ContainsID(AudioID id)
    {
        return ContainsID((int)id);
    }
}

[Serializable]
public struct AudioPair
{
    public AudioID audioID;
    public AudioClip clip;

    public int GetID { get { return (int)audioID; } }

    public static bool operator ==(AudioPair a, AudioPair b)
    {
        return a.audioID == b.audioID;
    }
    public static bool operator !=(AudioPair a, AudioPair b)
    {
        return a.audioID != b.audioID;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }
}
