using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour {
    public input thisInput;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        transform.Rotate(-thisInput.prevY, 0, 0); // = Quaternion.Euler(thisInput.prevY, 0, 0);
        if (transform.rotation.x < -100)
            transform.rotation = Quaternion.Euler(-100, 0, 0);
        if (transform.rotation.x > 50)
            transform.rotation = Quaternion.Euler(50, 0, 0);


    }
}
