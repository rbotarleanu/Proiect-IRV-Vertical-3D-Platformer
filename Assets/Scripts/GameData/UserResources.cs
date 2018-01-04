using System.Collections;
using System.Collections.Generic;

public class UserResources {

    public static int coins { get; private set; }
    public static int powerups { get; private set; }

    public delegate void CoinCollected();
    public static event CoinCollected OnCoinCollected;

    public delegate void PowerupPickedUp();
    public static event PowerupPickedUp OnPowerupPicked;

    public static void collectCoin()
    {
        ++coins;
        OnCoinCollected();
    }

    public static void pickPowerup()
    {
        ++powerups;
        //OnPowerupPicked();
    }

    public UserResources()
    {

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
