using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

    private const float rotationSpeed = 50;

    private string GameObjectName;

	// Use this for initialization
	void Start () {
        GameObjectName = gameObject.name;
        List<string> CollectedCoinsNames = GameData.GetInstance().CollectedCoins;
        if (CollectedCoinsNames != null && CollectedCoinsNames.Contains(GameObjectName))
            Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        // Rotate the coin around its own axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameData.CollectCoin(GameObjectName);
        AudioManager.PlayOnce(AudioManager.AudioChannel.SFX, AudioResources.Instance.CoinPickUp);
        Destroy(gameObject);
    }

}
