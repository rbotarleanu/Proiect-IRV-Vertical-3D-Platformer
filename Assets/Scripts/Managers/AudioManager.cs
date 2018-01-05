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
        Voice,
        Music
    }

    public static float sfxIntensity { get; private set; }
    public static float musicIntensity { get; private set; }
    public static float voiceIntensity { get; private set; }

    public static void Init()
    {
        sfxIntensity = PlayerPrefs.GetFloat("sfxIntensity", 0.5f);
        musicIntensity = PlayerPrefs.GetFloat("musicIntensity", 0.5f);
        voiceIntensity = PlayerPrefs.GetFloat("voiceIntensity", 0.5f);
    }

    public static void savePrefs()
    {
        PlayerPrefs.SetFloat("sfxIntensity", sfxIntensity);
        PlayerPrefs.SetFloat("musicIntensity", musicIntensity);
        PlayerPrefs.SetFloat("voiceIntensity", voiceIntensity);
    }

    public static void changeIntensity(AudioChannel channel, float intensity)
    {

    }

    // Update is called once per frame
    void Update () {
		
	}
}
