using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AOEAttack : NetworkBehaviour {

    public int damage;
    public multiSetup thisMulti;
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
                int target = other.GetComponent<hitboxManager>().getId();
                //stream.collider.gameObject.SendMessage("getId",  SendMessageOptions.DontRequireReceiver, out target,);
                thisMulti.CmdstrikeById(damage, target);

                Debug.Log("ready");
                   // other.GetComponent<hitboxManager>().strike(dmg);
                    Destroy(gameObject);

                }
                catch
                {
                    Debug.Log("noope");
                }
            

        }
    }

}
