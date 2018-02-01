using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreComponent : MonoBehaviour {

    public Text score;

	// Use this for initialization
	void Start () {
        score.text = "0";
        gameObject.SetActive(false);

        GameManager.OnStateChange += StateChangeHandler;
        GameData.OnCoinCollected += CoinCollectedHandler;
	}

    private void StateChangeHandler(GameManager.GameState newState)
    {
        if (newState == GameManager.GameState.InGame)
        {
            gameObject.SetActive(true);
        }
        else
        {
            if (gameObject.activeSelf == true)
                gameObject.SetActive(false);
        }
    }

    private void CoinCollectedHandler()
    {
        score.text = GameData.GetInstance().PickedUpCoins.ToString();
    }

    void OnDestroy()
    {
        GameManager.OnStateChange -= StateChangeHandler;
        GameData.OnCoinCollected -= CoinCollectedHandler;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
