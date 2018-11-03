using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class generalMultiplayer : NetworkBehaviour
{

    public static List< multiSetup> PlayerList;
    // Use this for initialization
    void Start () {
        PlayerList = new List<multiSetup>();
        //if (!isServer)
        //    this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //[Command]
    //public void CmdSpawn(int effect, Vector3 position, Quaternion rotation)
    //{
    //    Debug.Log("yup");
    //    GameObject missile = Instantiate(missileList[effect], position, rotation);
    //    NetworkServer.Spawn(missile);


    //}
}
