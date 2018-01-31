using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Slider loadingBar; // set from the unity editor

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false); // loading screen is only active during async loading

        LevelManager.OnLoadStart += LoadStartHandler;
        LevelManager.OnLoadChange += LoadChangeHandler;
    }

    private void LoadStartHandler()
    {
        gameObject.SetActive(true);
    }

    private void LoadChangeHandler(float value)
    {
        loadingBar.value = value;
        if (value == 1)
        {
            gameObject.SetActive(false);
        }
    }

    void OnDestroy()
    {
        LevelManager.OnLoadStart -= LoadStartHandler;
        LevelManager.OnLoadChange -= LoadChangeHandler;
    }
}