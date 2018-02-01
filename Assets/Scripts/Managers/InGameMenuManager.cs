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
    public Text dialogText;
    
    private const string exitToMainMenuText = "You will exit to the main menu. Are you sure?";
    private const string saveGameText = "Are you sure you wish to save the game state?";

    private enum DialogBacklink { // specifies which button was pressed for the common dialog
        ExitToMainMenu,
        SaveGame,
        None
    };

    private DialogBacklink activeButton = DialogBacklink.None;

    private void Start()
    {
        gameObject.SetActive(false);
        dialog.SetActive(false);

        Cursor.SetCursor(cursorTexture, new Vector2(6, 0), CursorMode.ForceSoftware);

        saveGameButton.onClick.AddListener(() =>
        {
            activeButton = DialogBacklink.SaveGame;
            dialogText.text = saveGameText;
            dialog.SetActive(true);
        });

        exitToMainMenuButton.onClick.AddListener(() =>
        {
            activeButton = DialogBacklink.ExitToMainMenu;
            dialogText.text = exitToMainMenuText;
            dialog.SetActive(true);
        });

        dialogYes.onClick.AddListener(() => {
            if (activeButton == DialogBacklink.ExitToMainMenu)
            {
                dialogText.text = "";
                dialog.SetActive(false);
                GameManager.LoadMainMenu();
            } else
            {
                GameData.Serialize();
            }
        });

        dialogNo.onClick.AddListener(() =>
        {
            dialogText.text = "";
            dialog.SetActive(false);
        });

        GameManager.OnStateChange += StateChangeHandler;
    }

    private void StateChangeHandler(GameManager.GameState newState)
    {
        if (newState == GameManager.GameState.InGameMenu)
        {
            gameObject.SetActive(true);
        }
        else if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }

    void OnDestroy()
    {
        GameManager.OnStateChange -= StateChangeHandler;
    }

    void Update()
    {
       
    }
}
