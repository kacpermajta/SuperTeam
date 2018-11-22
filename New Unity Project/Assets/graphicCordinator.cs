using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graphicCordinator : MonoBehaviour {
    [SerializeField]
    ParticleSystem GeneralParticles;
     ParticleSystem.EmissionModule[] Particles;
    ParticleSystem.ShapeModule[] Shapes;
//    parti

    // Use this for initialization
    void Start () {
        if (GeneralParticles != null)
        {
            Particles = new ParticleSystem.EmissionModule[2];
            Particles[0] = GeneralParticles.emission;
            try
            {
                Shapes[0] = GeneralParticles.shape;
            }
            catch
            {

            }
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

    public void Aim (Vector3 target)
    {
        if(Shapes[0].enabled==true)
        {

            Shapes[0].length = Vector3.Distance(transform.position, target);
            transform.LookAt(target);

        }
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
