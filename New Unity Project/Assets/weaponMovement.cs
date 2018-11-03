using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponMovement : MonoBehaviour {
    public enum wpnType {rifle, pistol, sword, punch, ray, flame};
    public wpnType type;
    public input thisInput;
    public multiSetup thisMulti;
    public GameObject effect;
//    public ParticleSystem gunfire;
    public int cooldown;
 //   ParticleSystem.EmissionModule emiter;
    bool prevR;
    // Use this for initialization
    void Start () {
 //       emiter = gunfire.emission;
        cooldown = 0;
        prevR = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (thisMulti == null)
            return;
        if (thisInput.aimR)
        {
            transform.Rotate(0, 1.5f*thisInput.prevX, 0);


        }
        else
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        switch (type)
        {
            case wpnType.ray:
                if (prevR != thisInput.attackR)
                {
                    Debug.Log("current value: " + thisInput.attackR + ", prev: " + prevR);
                    if (thisInput.attackR)
                    {
                        //                       thisMulti.CmdEnableParticle(0, transform.position, transform.rotation);
                        thisMulti.CmdTurn(true);
                    }
                    else
                    {
                        //                       thisMulti.CmdDisableParticle(0, transform.position, transform.rotation);
                        thisMulti.CmdTurn(false);
                    }
                }
                break;

            case wpnType.rifle:
                if (thisInput.attackR)
                    if (cooldown == 0)
                    {
//                        Debug.Log("pew");
                        thisMulti.CmdSpawn(0, transform.position, transform.rotation);

                        cooldown = 20;
                    }
                break;
            case wpnType.pistol:
                if (thisInput.attackR)
                    if (cooldown == 0)
                    {
 //                       Debug.Log("pew");
                        thisMulti.CmdSpawn(0, transform.position, transform.rotation);

                        cooldown = 60;
                    }
                break;

        }
        prevR = thisInput.attackR;
    }
    void FixedUpdate()
    {
        if (cooldown > 0)
        {
            cooldown--;

        }
        


    }

    public void strike()
    {
        switch (type)
        {
            case wpnType.ray:
                Debug.Log("kula");

                
                Debug.Log("emitujze");

                cooldown = 100;
                break;

            case wpnType.rifle:
                
                break;
    }
    }
}
