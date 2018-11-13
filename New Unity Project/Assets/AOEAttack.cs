using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AOEAttack : NetworkBehaviour {

    public int dmg;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerStay(Collider other)

    {
        if (other.tag != "nonexist")
        {
            



                try
                {
                    Debug.Log("ready");
                    other.GetComponent<hitboxManager>().strike(dmg);
                    Destroy(gameObject);

                }
                catch
                {
                    Debug.Log("noope");
                }
            

        }
    }

}
