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

        GameManager.OnStateChange += ((GameManager.GameState newState) => {
            if (newState == GameManager.GameState.InGame) {
                gameObject.SetActive(true);
            } else {
                if (gameObject.activeSelf == true)
                    gameObject.SetActive(false);
            }
        });

        UserResources.OnCoinCollected += (() =>
        {
            score.text = UserResources.coins.ToString();
        });
	}
	
    // Update is called once per frame
	void Update () {
		
	}
}
