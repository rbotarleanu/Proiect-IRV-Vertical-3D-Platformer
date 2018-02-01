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

    public AudioClip Scene1BgMusic;
    public AudioClip Scene2BgMusic;
    private static AudioSource AudioComponent;

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

            // Initialize sub-managers
            AudioManager.Init();

            AudioComponent = GetComponent<AudioSource>();
            AudioManager.Play(AudioManager.AudioChannel.MUSIC, AudioComponent, true);
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

	// Update is called once per frame
	void Update () {
        bool sceneLoaded = false;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            LevelManager.LoadSceneAsync("Scene1");
            sceneLoaded = true;
        }

        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            Debug.Log("SFX is: " + AudioManager.SfxIntensity);
            Debug.Log("Voice is: " + AudioManager.VoiceIntensity);
            Debug.Log("Music is: " + AudioManager.MusicIntensity);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            LevelManager.LoadSceneAsync("Scene2");
            ChangeMusic(GetInstance().Scene2BgMusic);
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

    private static void ChangeMusic(AudioClip soundClip)
    {
        AudioComponent.clip = soundClip;
        AudioManager.Stop(AudioManager.AudioChannel.MUSIC, AudioComponent);
        AudioManager.Play(AudioManager.AudioChannel.MUSIC, AudioComponent, true);
    }

    public static void StartNewGame()
    {
        LevelManager.LoadSceneAsync("Scene1");
        currentState = GameState.InGame;

        ChangeMusic(GetInstance().Scene1BgMusic);

        OnStateChange(currentState);
    }

    public static void LoadMainMenu()
    {
        SetGameState(GameState.MainMenu);
        AudioManager.Stop(AudioManager.AudioChannel.MUSIC, AudioComponent);
        LevelManager.LoadSceneAsync("MainMenu");
    }

    public static void LoadLastGame()
    {
        // TODO
    }

 }
