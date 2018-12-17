using System.Text;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Encoders;
using BayatGames.SaveGameFree.Serializers;
using BayatGames.SaveGameFree.Types;

/// <summary>
/// Save format.
/// </summary>
public enum SaveFormat
{
    XML,
    JSON,
    Binary
}

public class SaveManager : Singleton<SaveManager> {
    
    [Header("Save Settings")]
    [SerializeField]
    private string saveId = "12345678";
    [SerializeField]
    private bool encode = true;
    [SerializeField]
    private string encodePassword = "87654321";
    [SerializeField]
    private Encoding encoding;
    [SerializeField]
    private SaveFormat format = SaveFormat.Binary;
    [SerializeField]
    private SaveGamePath savePath = SaveGamePath.PersistentDataPath;
    private ISaveGameSerializer serializer;
    private ISaveGameEncoder encoder;
    
    public GameSave GameSave;
    public GameSave defaultSave = new GameSave();
    public bool useDefaultSave;

    [Header("Language")]
    public Language language;

    protected override void Init ()
    {
        switch (format)
        {
            case SaveFormat.Binary:
                serializer = new SaveGameBinarySerializer();
                break;
            case SaveFormat.JSON:
                serializer = new SaveGameJsonSerializer();
                break;
            case SaveFormat.XML:
                serializer = new SaveGameXmlSerializer();
                break;
        }
        encoder = SaveGame.Encoder;
        Load();
    }

    public void Save()
    {
        SaveGame.Save(saveId, GameSave, encode, encodePassword, serializer, encoder, encoding, savePath);
    }

    public void Load()
    {
        if (SaveGame.Exists(saveId) && !useDefaultSave)
        {
            GameSave = SaveGame.Load(saveId, defaultSave, encode, encodePassword, serializer, encoder, encoding, savePath);
        }
        else
        {
            RestartSave();
        }
    }
    
    public void RestartSave()
    {
        GameSave = defaultSave;
    }

    public static void AddEventClear(string eventName)
    {
        if (eventName == null || eventName.Length <= 0)
            return;

        if (!Instance.GameSave.clearedEvents.Contains(eventName))
        {
            Instance.GameSave.clearedEvents.Add(eventName);
        }
    }

    public static bool EventCleared(string eventName)
    {
        return Instance.GameSave.clearedEvents.Contains(eventName);
    }
}
