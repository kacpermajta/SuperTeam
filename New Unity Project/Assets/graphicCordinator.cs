using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graphicCordinator : MonoBehaviour {
    [SerializeField]
    ParticleSystem GeneralParticles;
     ParticleSystem.EmissionModule[] Particles;
//    parti

    // Use this for initialization
    void Start () {
        if (GeneralParticles != null)
        {
            Particles = new ParticleSystem.EmissionModule[2];
            Particles[0] = GeneralParticles.emission;
            Particles[0].enabled = false;
            try
            {
                Particles[1] = GeneralParticles.transform.GetChild(0).GetComponent<ParticleSystem>().emission;
                Particles[1].enabled = false;
            }
            catch
            { }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
    public void Turn(bool value)
    {
        Particles[0].enabled = value;
        try
        {
            Particles[1].enabled = value;
        }
        catch
        {
        }
        
    }
}
