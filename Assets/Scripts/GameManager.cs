using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/* Singleton that controls game managers and scene flow. */
public class GameManager : MonoBehaviour {

    /* Only one instance per game. */
    private static GameManager instance;

    public static GameManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        } else
        {
            Destroy(gameObject);
        }
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LevelManager.LoadSceneAsync("Scene1");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            LevelManager.LoadSceneAsync("Scene2");
        }
    }
 }
