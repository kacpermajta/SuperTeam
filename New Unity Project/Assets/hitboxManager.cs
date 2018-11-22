using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class hitboxManager : NetworkBehaviour {

    public multiSetup motherScript;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void strike(int dmg, int team)
    {
        
        motherScript.Cmdstrike(dmg, team);
        


    }
    public int getId()
    {
        return (motherScript.RemnetId);
    }
}
