using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuManager : MonoBehaviour {

    public Texture2D cursorTexture;
    public GameObject dialog;
    public Button exitToMainMenuButton;
    public Button saveGameButton;
    public Button dialogYes;
    public Button dialogNo;

    private void Start()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(6, 0), CursorMode.ForceSoftware);

        gameObject.SetActive(false);

        GameManager.OnStateChange += ((GameManager.GameState newState) =>
        {
            if (newState == GameManager.GameState.InGameMenu)
            {
                gameObject.SetActive(true);
            }
            else if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Confined;
            }
        });
    }

    void Update()
    {
       
    }
}
