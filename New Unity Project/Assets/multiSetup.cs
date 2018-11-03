using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class multiSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] localComponents;

    
    Camera sceneCamera;

    public GameObject[] missileList;
    GameObject weaponModel;
    [SerializeField]
    graphicCordinator[] weaponGraph;
    public int RemnetId, weaponNum;

    // Use this for initialization
    void Start () {
        RemnetId = int.Parse(GetComponent<NetworkIdentity>().netId.ToString()); 

        if (!isLocalPlayer)
        {
            for (int i = 0; i < localComponents.Length; i++)
            {
                localComponents[i].enabled = false;

            }
        }
        else
        {
            PersistantSettings.chosenWpn.script.thisMulti = this;
            PersistantSettings.chosenWpn.script.thisInput = (input)localComponents[1];
            Vector3 positionOf = PersistantSettings.chosenWpn.prefab.transform.localPosition;
            PersistantSettings.chosenWpn.prefab.transform.parent = transform.GetChild(6);
            PersistantSettings.chosenWpn.prefab.transform.localPosition= positionOf;
            //localComponents[11] = PersistantSettings.chosenWpn.script;
            gameObject.GetComponent<NetworkTransformChild>().target = PersistantSettings.chosenWpn.prefab.transform;
            cameraSpotHandler thisHandler = (cameraSpotHandler)localComponents[10];
            thisHandler.spotR = PersistantSettings.chosenWpn.CameraSpot.transform;
            weaponGraph[0] = PersistantSettings.chosenWpn.graphics;
            CmdSpawnWpn(PersistantSettings.chosenWpn.num);

            sceneCamera = Camera.main;
            if (sceneCamera != null)
                sceneCamera.gameObject.SetActive(false);
 

        }
	}
	
	// Update is called once per frame
	void Update () {


		
	}
    private void OnDisable()
    {
        if(sceneCamera!=null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }


    [Command]

    public void CmdSpawn(int effect, Vector3 position, Quaternion rotation)
    {
        Debug.Log("yup");
        GameObject missile = Instantiate(missileList[effect], position, rotation);
        NetworkServer.Spawn(missile);
        

    }
    [Command]

    public void CmdSpawnWpn(int num)
    {
        weaponNum = num;
        Debug.Log("nadal gracz: " + RemnetId);
        SpawnOtherWpn(RemnetId, num);
 //       foreach (multiSetup sinPlayer in generalMultiplayer.PlayerList)

 //           TargetSpawnEachOtherWpn(NetworkServer.connections[RemnetId]);
        RpcSpawnOtherWpn(RemnetId, num);

        generalMultiplayer.PlayerList.Add(this);

    }
    [TargetRpc]
    public void TargetSpawnEachOtherWpn(NetworkConnection thisConnection)
    {
        SpawnOtherWpn(RemnetId, weaponNum);


    }


    [ClientRpc]
    public void RpcSpawnOtherWpn(int ID, int num)
    {
        SpawnOtherWpn(RemnetId, num);


    }
    public GameObject SpawnOtherWpn(int ID, int num)
    {
        Debug.Log("odebral gracz: " + RemnetId + " od gracza " + ID);
       
            GameObject newWpn = Instantiate(PersistantSettings.weaponList[num], transform.GetChild(6));
            weaponGraph[0] = newWpn.GetComponent<graphicCordinator>();
            newWpn.GetComponent<weaponMovement>().enabled = false; ;
            return newWpn;
        
        
    }
    [Command]
    public void CmdTurn(bool value)
    {
        //Debug.Log("current value: " + value);
        RpcTurn(value);
        weaponGraph[0].Turn(value);

    }
    [ClientRpc]
    public void RpcTurn(bool value)
    {
        weaponGraph[0].Turn(value);
    }








}
