using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionCheck : MonoBehaviour {
    public bool value, absvalue;
    public string reqTag;

	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerStay(Collider other)
    {
        absvalue = true;
        if (reqTag == "" || other.tag== reqTag)
            value = true;

    }
}
