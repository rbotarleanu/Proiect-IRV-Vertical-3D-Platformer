using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

using System.Xml.Serialization;

[XmlRoot("GameData")]
public class GameData {

    private static GameData Instance = new GameData();

    private static string XMLFileName = "./GameData.xml";
    private static string serializationPath = Application.persistentDataPath + XMLFileName;

    [XmlElement("PickedUpCoins")]
    public int PickedUpCoins;
    [XmlElement("PickedUpPowerUps")]
    public int PickedUpPowerUps;

    public delegate void CoinCollected();
    public static event CoinCollected OnCoinCollected;

    public delegate void PowerupPickedUp();
    public static event PowerupPickedUp OnPowerupPicked;

    public static void CollectCoin()
    {
        ++GetInstance().PickedUpCoins;
        OnCoinCollected();
    }

    public static void PickPowerUp()
    {
        ++GetInstance().PickedUpPowerUps;
    }

    public GameData()
    {

    }

    public static GameData GetInstance()
    {
        return Instance;
    }

	// Use this for initialization
	void Start () {
	}

    public static void Serialize()
    {
        var serializer = new XmlSerializer(typeof(GameData));
        var stream = new FileStream(serializationPath, FileMode.Create);
        serializer.Serialize(stream, Instance);
        stream.Close();
        Debug.Log("Saved to: " + serializationPath);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
