using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class AudioResources: MonoBehaviour {

    public AudioSource CoinPickUp;

    public static AudioResources Instance;

    // Use this for initialization
    void Start () {
        Instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
