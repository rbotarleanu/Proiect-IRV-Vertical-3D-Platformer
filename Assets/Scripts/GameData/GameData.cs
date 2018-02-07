using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

using System.Xml.Serialization;

[XmlRoot("GameData")]
public class GameData {

    private static GameData Instance = new GameData();

    private static string XMLFileName = "./GameData.xml";
    private static string serializationPath = Application.persistentDataPath + XMLFileName;

    public delegate void reload();
    public static event reload OnReload;

    [XmlElement("PickedUpCoins")]
    public int PickedUpCoins;
    [XmlElement("SFXVolume")]
    public float SfxVolume;
    [XmlElement("MusicVolume")]
    public float MusicVolume;
    [XmlElement("VoiceVolume")]
    public float VoiceVolume;
    [XmlElement("CurrentScene")]
    public string CurrentScene;
    [XmlArray("CollectedCoins")]
    public List<string> CollectedCoins;
    [XmlElement("PlayerPosition")]
    public Vector3 PlayerPosition;

    public delegate void CoinCollected(bool onReload);
    public static event CoinCollected OnCoinCollected;

    public static void CollectCoin(string coin)
    {
        GameData instance = GetInstance();
        ++instance.PickedUpCoins;

        if (instance.CollectedCoins == null)
            instance.CollectedCoins = new List<string>();
        instance.CollectedCoins.Add(coin);
        OnCoinCollected(false);
    }

    public void Reset()
    {
        PickedUpCoins = 0;
        CollectedCoins.Clear();
        OnCoinCollected(true);
    }

    public static GameData GetInstance()
    {
        return Instance;
    }

    // Use this for initialization
    void Start() {
    }

    public void SyncAudioSettingsWithAudioManager()
    {
        SfxVolume = AudioManager.SfxIntensity;
        MusicVolume = AudioManager.MusicIntensity;
        VoiceVolume = AudioManager.VoiceIntensity;
    }

    public static void Serialize()
    {
        GetInstance().SyncAudioSettingsWithAudioManager();
        GetInstance().CurrentScene = GameManager.currentScene;
        GetInstance().PlayerPosition = GameManager.GetPlayerPosition();

        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        FileStream stream = new FileStream(serializationPath, FileMode.Create);
        serializer.Serialize(stream, Instance);
        stream.Close();
    }

    public static void Deserialize()
    {
        if (!File.Exists(serializationPath))
            return;

        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        FileStream stream = new FileStream(serializationPath, FileMode.Open);
        try
        {
            Instance = serializer.Deserialize(stream) as GameData;
            stream.Close();
            Instance.UpdateGameData();
        } catch (IOException e)
        {
            Debug.LogError("Failed to deserialize game data: " + e);
        } finally
        {
            stream.Close();
        }
    }

    public void UpdateGameData()
    {
        OnCoinCollected(true);

        AudioManager.ChangeChannelIntensity(AudioManager.AudioChannel.SFX, SfxVolume);
        AudioManager.ChangeChannelIntensity(AudioManager.AudioChannel.MUSIC, MusicVolume);
        AudioManager.ChangeChannelIntensity(AudioManager.AudioChannel.VOICE, VoiceVolume);

        OnReload();

        GameManager.HandleGameReload(CurrentScene, PlayerPosition);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
