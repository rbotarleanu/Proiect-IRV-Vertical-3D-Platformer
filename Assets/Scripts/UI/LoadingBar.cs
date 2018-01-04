using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Slider loadingBar; // set from the unity editor

    void Start()
    {
        gameObject.SetActive(false); // loading screen is only active during async loading

        LevelManager.OnLoadStart += () => // show the loading screen during loading
        {
            gameObject.SetActive(true);
        };

        LevelManager.OnLoadChange += ((float value) => // update slider during loading
        {
            loadingBar.value = value;
            Debug.Log("Loading bar value: " + value);
            if (value == 1)
            {
                gameObject.SetActive(false);
            }
        });
    }
}