using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;


/* Singleton that controls game managers and scene flow. */
public class GameManager : MonoBehaviour
{

    /* Only one instance per game. */
    private static GameManager instance;

    public delegate void StateChange(GameState newState);
    public static event StateChange OnStateChange;

    public AudioClip Scene1BgMusic;
    public AudioClip Scene2BgMusic;
    private static AudioSource AudioComponent;

    private FirstPersonController sceneFPSController;
    private AudioSource sceneCharacterAudio;

    private bool NeedToUpdatePlayerPosition = false;
    private Vector3 NewPlayerPosition;

    public enum GameState
    {
        MainMenu,
        InGame,
        InGameMenu
    }

    private static GameState currentState;
    public static string currentScene = "MainMenu";
    
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
            LevelManager.OnLoadChange += WaitForSceneLoad;
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
            GetInstance().sceneFPSController.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (newState == GameState.InGame)
        {
            GetInstance().sceneFPSController.enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    private void WaitForSceneLoad(float value)
    {
        if (value < 1)
            return;

        AudioManager.Stop(AudioManager.AudioChannel.VOICE, sceneCharacterAudio);

        // Scene has been loaded, we can bind the FirstPersonCharacter component
        GameObject fpsObject = GameObject.Find("FPSController");
        if (fpsObject != null)
        {
            sceneFPSController = fpsObject.GetComponent<FirstPersonController>(); sceneCharacterAudio = fpsObject.GetComponent<AudioSource>();
            sceneCharacterAudio = fpsObject.GetComponent<AudioSource>();
            AudioManager.Play(AudioManager.AudioChannel.VOICE, sceneCharacterAudio, false);

            if (NeedToUpdatePlayerPosition)
            {
                sceneFPSController.transform.position = NewPlayerPosition;
                NeedToUpdatePlayerPosition = false;
            }
        }
    }

    public static void LoadScene(string sceneName)
    {
        LevelManager.LoadSceneAsync(sceneName);
        currentState = GameState.InGame;
        currentScene = sceneName;
        AudioClip CorrectClip = sceneName.Equals("Scene1") ? GetInstance().Scene1BgMusic :
                        GetInstance().Scene2BgMusic;
        ChangeMusic(CorrectClip);

        currentState = GameState.InGame;
        OnStateChange(currentState);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            LoadScene("Scene1");
        if (Input.GetKeyDown(KeyCode.E))
            LoadScene("Scene2");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.InGameMenu)
                SetGameState(GameState.InGame);
            else if (currentState == GameState.InGame)
                SetGameState(GameState.InGameMenu);
        }
    }

    public static void UpdatePlayerPosition(Vector3 playerPosition)
    {
        GetInstance().sceneFPSController.transform.position = playerPosition;
    }

    private static void ChangeMusic(AudioClip soundClip)
    {
        AudioComponent.clip = soundClip;
        AudioManager.Stop(AudioManager.AudioChannel.MUSIC, AudioComponent);
        AudioManager.Play(AudioManager.AudioChannel.MUSIC, AudioComponent, true);
    }

    public static void StartNewGame()
    {
        LoadScene("Scene1");
    }

    public static void LoadMainMenu()
    {
        SetGameState(GameState.MainMenu);
        AudioManager.Stop(AudioManager.AudioChannel.MUSIC, AudioComponent);
        LevelManager.LoadSceneAsync("MainMenu");
    }

    public static void LoadLastGame()
    {
        GameData.Deserialize();
    }

    public static void HandleGameReload(string scene, Vector3 playerPosition)
    {
        if (AudioManager.musicSources.Contains(AudioComponent))
            AudioManager.Stop(AudioManager.AudioChannel.MUSIC, AudioComponent);
        AudioManager.Play(AudioManager.AudioChannel.MUSIC, AudioComponent, true);
            
        GetInstance().NeedToUpdatePlayerPosition = true;
        GetInstance().NewPlayerPosition = playerPosition;
        LoadScene(scene);
    }

    public static Vector3 GetPlayerPosition()
    {
        return GetInstance().sceneFPSController.transform.position;
    }

    public static Quaternion GetPlayerRotation()
    {
        return GetInstance().sceneFPSController.transform.rotation;
    }

}
