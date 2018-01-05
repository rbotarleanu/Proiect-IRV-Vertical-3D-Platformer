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

	// Use this for initialization
	void Start () {
        sfxSlider.onValueChanged.AddListener((float newValue) =>
        {
            sfxDisplay.text = ((int) (newValue * 100)).ToString();
        });

        voiceSlider.onValueChanged.AddListener((float newValue) =>
        {
            voiceDisplay.text = ((int)(newValue * 100)).ToString();
        });

        musicSlider.onValueChanged.AddListener((float newValue) =>
        {
            musicDisplay.text = ((int)(newValue * 100)).ToString();
        });
    }

    // Update is called once per frame
    void Update () {
		
	}
}
