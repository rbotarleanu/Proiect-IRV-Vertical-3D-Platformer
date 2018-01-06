using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject primarySection;
    public GameObject audioSection;
    public GameObject newGameDialog;

    public Button newGameButton;
    public Button loadGameButton;
    public Button audioSettingsButton;
    public Button backToMainMenuButton;
    public Button yesButton;
    public Button noButton;
 
    public Texture2D cursorTexture;

    private void Start()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(1, 0), CursorMode.ForceSoftware);
        GameManager.SetGameState(GameManager.GameState.MainMenu);

        audioSection.SetActive(false); // shown only when button is clicked
        newGameDialog.SetActive(false);

        // Listen for new game state
        GameManager.OnStateChange += stateListener;

        InitializeButtonListeners();
    }

    private void stateListener(GameManager.GameState newState)
    {
        if (newState != GameManager.GameState.MainMenu)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        GameManager.OnStateChange -= stateListener;
    }

    private void InitializeButtonListeners()
    {
        audioSettingsButton.onClick.AddListener(() =>
        {
            primarySection.SetActive(false);
            audioSection.SetActive(true);
        });

        backToMainMenuButton.onClick.AddListener(() =>
        {
            audioSection.SetActive(false);
            primarySection.SetActive(true);
        });

        newGameButton.onClick.AddListener(() =>
        {
            primarySection.SetActive(false);
            newGameDialog.SetActive(true);
        });

        yesButton.onClick.AddListener(() =>
        {
            GameManager.StartNewGame();
        });

        noButton.onClick.AddListener(() => {
            newGameDialog.SetActive(false);
            primarySection.SetActive(true);
        });

        loadGameButton.onClick.AddListener(() =>
        {
            GameManager.LoadLastGame();
        });
    }
    
    void Update()
    {
    }

}
