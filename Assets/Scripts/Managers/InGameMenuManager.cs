using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class InGameMenuManager : MonoBehaviour {

    public Texture2D cursorTexture;
    public GameObject dialog;
    public GameObject inGameMenuPanel;
    public Button exitToMainMenuButton;
    public Button saveGameButton;
    public Button dialogYes;
    public Button dialogNo;

    private void Start()
    {
        gameObject.SetActive(false);
        
        Cursor.SetCursor(cursorTexture, new Vector2(6, 0), CursorMode.ForceSoftware);

        exitToMainMenuButton.onClick.AddListener(() =>
        {
            GameManager.SetGameState(GameManager.GameState.MainMenu);
            GameManager.LoadMainMenu();
        });

        GameManager.OnStateChange += ((GameManager.GameState newState) =>
        {
            if (newState == GameManager.GameState.InGameMenu)
            {
                gameObject.SetActive(true);
            }
            else if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
            }
        });
    }

    void Update()
    {
       
    }
}
