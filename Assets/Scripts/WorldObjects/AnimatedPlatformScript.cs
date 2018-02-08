using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedPlatformScript : MonoBehaviour {

    public GameObject Platform;
    private Animation PlatformAnimation;

	// Use this for initialization
	void Start () {
        PlatformAnimation = Platform.GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Start");
        PlatformAnimation.Play();
    }
}
