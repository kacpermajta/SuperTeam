using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitboxManager : MonoBehaviour {

    public characterController motherScript;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void strike(int dmg)
    {
        motherScript.strike(dmg);
    }
}
