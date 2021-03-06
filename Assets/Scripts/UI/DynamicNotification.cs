﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicNotification : MonoBehaviour
{
    public Text textPopup;
    public const float timeMessageIsDisplayed = 5; // seconds

    void Start()
    {
        gameObject.SetActive(false);

        GameData.OnCoinCollected += CoinCollectedHandler;
    }

    private void CoinCollectedHandler(bool onReload)
    {
        if (!onReload)
            displayMessage("Coin has been picked up!");
    }

    private void displayMessage(string message)
    {
        gameObject.SetActive(true);
        textPopup.text = message;
        GameManager.GetInstance().StartCoroutine(StopMessageDisplay());
    }

    private IEnumerator StopMessageDisplay()
    {
        yield return new WaitForSeconds(timeMessageIsDisplayed);
        
        textPopup.text = "";
        gameObject.SetActive(false);
    }
    
    void OnDestroy()
    {
        GameData.OnCoinCollected -= CoinCollectedHandler;
    }
}
	
    
