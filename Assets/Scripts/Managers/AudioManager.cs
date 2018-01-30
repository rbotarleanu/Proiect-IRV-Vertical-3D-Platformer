using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager {

	// Use this for initialization
	void Start () {
		
	}

    public enum AudioChannel
    {
        SFX,
        MUSIC,
        VOICE
    }

    public static float SfxIntensity { get; private set; }
    public static float MusicIntensity { get; private set; }
    public static float VoiceIntensity { get; private set; }

    public static void Init()
    {
        Debug.Log("Initializing");
        SfxIntensity = PlayerPrefs.GetFloat("sfxIntensity", 0.5f);
        MusicIntensity = PlayerPrefs.GetFloat("musicIntensity", 0.5f);
        VoiceIntensity = PlayerPrefs.GetFloat("voiceIntensity", 0.5f);
    }

    public static void SavePrefs()
    {
        PlayerPrefs.SetFloat("sfxIntensity", SfxIntensity);
        PlayerPrefs.SetFloat("musicIntensity", MusicIntensity);
        PlayerPrefs.SetFloat("voiceIntensity", VoiceIntensity);
    }
    
    public static void ChangeChannelIntensity(AudioChannel audioChannel, float value)
    {
        if (value < 0 || value > 1)
            return;

        switch (audioChannel)
        {
            case AudioChannel.SFX: SfxIntensity = value; break;
            case AudioChannel.MUSIC: MusicIntensity= value; break;
            case AudioChannel.VOICE: VoiceIntensity = value; break;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
