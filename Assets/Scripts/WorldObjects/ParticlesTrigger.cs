using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesTrigger : MonoBehaviour {

    public ParticleSystem particles;
    private bool particlesStarted = false;

	// Use this for initialization
	void Start () {
        particles.Stop();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!particlesStarted)
        {
            Debug.Log("Particles triggered.");
            particles.Play();
        }
    }
}
