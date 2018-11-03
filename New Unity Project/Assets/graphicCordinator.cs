using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graphicCordinator : MonoBehaviour {
    [SerializeField]
    ParticleSystem GeneralParticles;
     ParticleSystem.EmissionModule[] Particles;

    // Use this for initialization
    void Start () {
        Particles = new ParticleSystem.EmissionModule[2] ;
        Particles[0] = GeneralParticles.emission;
        Particles[1] = GeneralParticles.transform.GetChild(0).GetComponent<ParticleSystem>().emission;
        Particles[0].enabled = false;
        Particles[1].enabled = false;

    }

    // Update is called once per frame
    void Update () {
		
	}
    public void Turn(bool value)
    {
        Particles[0].enabled = value;
        Particles[1].enabled = value;
    }
}
