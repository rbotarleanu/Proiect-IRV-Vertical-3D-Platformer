using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlidersController : MonoBehaviour {

    public Slider sfxSlider;
    public Slider voiceSlider;
    public Slider musicSlider;

    public Text sfxDisplay;
    public Text voiceDisplay;
    public Text musicDisplay;

    private static string GetDisplayValueFromIntensity(float value)
    {
        return ((int) (value * 100)).ToString();
    }

    public void SetValuesToGlobal()
    {
        sfxDisplay.text = GetDisplayValueFromIntensity(AudioManager.SfxIntensity);
        sfxSlider.value = AudioManager.SfxIntensity;
        musicDisplay.text = GetDisplayValueFromIntensity(AudioManager.MusicIntensity);
        musicSlider.value = AudioManager.MusicIntensity;
        voiceDisplay.text = GetDisplayValueFromIntensity(AudioManager.VoiceIntensity);
        voiceSlider.value = AudioManager.VoiceIntensity;
    }

    // Use this for initialization
    void Start () {
        SetValuesToGlobal();

        GameManager.OnStateChange += ((GameManager.GameState newState) =>
        { // this needs to be done on state change since the audio manager is only init once per game
            if (newState == GameManager.GameState.InGameMenu)
                SetValuesToGlobal();
        });

        // Add listeners
        sfxSlider.onValueChanged.AddListener((float newValue) =>
        {
            AudioManager.ChangeChannelIntensity(AudioManager.AudioChannel.SFX, newValue);
            sfxSlider.value = newValue;
            sfxDisplay.text = GetDisplayValueFromIntensity(newValue);
            float audioManagerValue = AudioManager.SfxIntensity;
        });

        voiceSlider.onValueChanged.AddListener((float newValue) =>
        {
            AudioManager.ChangeChannelIntensity(AudioManager.AudioChannel.VOICE, newValue);
            voiceSlider.value = newValue;
            voiceDisplay.text = GetDisplayValueFromIntensity(newValue);
        });

        musicSlider.onValueChanged.AddListener((float newValue) =>
        {
            AudioManager.ChangeChannelIntensity(AudioManager.AudioChannel.MUSIC, newValue);
            musicSlider.value = newValue;
            musicDisplay.text = GetDisplayValueFromIntensity(newValue);
        });
    }

    // Update is called once per frame
    void Update () {
		
	}
}
