using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;


/* Singleton that controls game managers and scene flow. */
public class GameManager : MonoBehaviour {

    /* Only one instance per game. */
    private static GameManager instance;

    public delegate void StateChange(GameState newState);
    public static event StateChange OnStateChange;

    public enum GameState {
        MainMenu,
        InGame,
        InGameMenu
    }

    private static GameState currentState;

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
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public static void SetGameState(GameState newState)
    {
        currentState = newState;
        if (OnStateChange != null)
            OnStateChange(currentState);

        if (newState == GameState.InGameMenu)
        {
            GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        } else if (newState == GameState.InGame)
        {
            GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    private void Init()
    {
    }

	// Update is called once per frame
	void Update () {
        bool sceneLoaded = false;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            LevelManager.LoadSceneAsync("Scene1");
            sceneLoaded = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            LevelManager.LoadSceneAsync("Scene2");
            sceneLoaded = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.InGameMenu)
                SetGameState(GameState.InGame);
            else if (currentState == GameState.InGame)
                SetGameState(GameState.InGameMenu);
        }

        if (sceneLoaded)
        {
            if (currentState != GameState.InGame)
            {
                currentState = GameState.InGame;
                OnStateChange(currentState);
            }
        }
    }

    public static void StartNewGame()
    {
        LevelManager.LoadSceneAsync("Scene1");
        currentState = GameState.InGame;
        OnStateChange(currentState);
    }

    public static void LoadMainMenu()
    {
        LevelManager.LoadScene("MainMenu");
    }

    public static void LoadLastGame()
    {
        // TODO
    }

 }
