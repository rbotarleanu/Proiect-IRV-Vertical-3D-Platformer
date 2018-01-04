using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreComponent : MonoBehaviour {

    public Text score;

	// Use this for initialization
	void Start () {
        score.text = "0";
        UserResources.OnCoinCollected += (() =>
        {
            score.text = UserResources.coins.ToString();
        });
	}
	
    // Update is called once per frame
	void Update () {
		
	}
}
