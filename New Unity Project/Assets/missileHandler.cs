﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class missileHandler : NetworkBehaviour {

    public Rigidbody thisBody;
    public GameObject hitParticle;
    public int dmg;
	// Use this for initialization
	void Start () {
        
        thisBody.AddRelativeForce(0, 0, 2000);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerStay(Collider other)

    {
        if (other.tag != "nonexist")
        {
            if (hitParticle != null)
            {
                GameObject hitInstance = Instantiate(hitParticle, transform.position, transform.rotation);
                Destroy(hitInstance, 3);
            }
            if (isServer)
            {
                

                try
                {
                    Debug.Log("ready");
                    other.GetComponent<hitboxManager>().strike(dmg);

                }
                catch
                {
                    Debug.Log("noope");
                }
            }
            Destroy(gameObject);
        }
    }



}
