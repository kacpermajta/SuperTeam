using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    int health;
    public Text healthbar;
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

            CmdSpawnWpn(PersistantSettings.chosenWpn.num, GetComponent<NetworkIdentity>());

            sceneCamera = Camera.main;
            if (sceneCamera != null)
                sceneCamera.gameObject.SetActive(false);
 

        }
        health = 34;
        healthbar.text = health.ToString();
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
        //Debug.Log("yup");
        GameObject missile = Instantiate(missileList[effect], position, rotation);
        NetworkServer.Spawn(missile);
        

    }
    [Command]

    public void CmdSpawnMissile(int effect, int dmg, Vector3 position, Quaternion rotation)
    {
        //Debug.Log("yup");
        GameObject missile = Instantiate(missileList[effect], position, rotation);
        missile.GetComponent<missileHandler>().dmg = dmg;
        NetworkServer.Spawn(missile);


    }
    [Command]

    public void CmdSpawnWpn(int num, NetworkIdentity target)
    {
        weaponNum = num;
        Debug.Log("nadal gracz: " + RemnetId);
        //        SpawnOtherWpn(RemnetId, num);
        foreach (multiSetup sinPlayer in generalMultiplayer.PlayerList)
        {
            sinPlayer.TargetSpawnEachOtherWpn(target.connectionToClient, sinPlayer.weaponNum);//0 do zmiany
        }
        RpcSpawnOtherWpn(num);
        
        generalMultiplayer.PlayerList.Add(this);

    }
    [TargetRpc]
    public void TargetSpawnEachOtherWpn(NetworkConnection thisConnection, int targetWpn)
    {
        SpawnOtherWpn(targetWpn);


    }


    [ClientRpc]
    public void RpcSpawnOtherWpn(int num)
    {
        SpawnOtherWpn(num);


    }
    public GameObject SpawnOtherWpn(int num)
    {
        Debug.Log("I built a gun");
        GameObject newWpn = Instantiate(PersistantSettings.weaponList[num], transform.GetChild(6));
        weaponMovement script = newWpn.GetComponent<weaponMovement>();
        if (!isLocalPlayer)
        {
            script.enabled = false;
        }
        else
        {
             script.thisMulti = this;
            script.thisInput = (input)localComponents[1];
            cameraSpotHandler thisHandler = (cameraSpotHandler)localComponents[9];
            thisHandler.spotR = newWpn.transform.GetChild(0);
        }
            
        

        gameObject.GetComponent<NetworkTransformChild>().target = newWpn.transform;
        weaponGraph[0] = newWpn.GetComponent<graphicCordinator>();
        Debug.Log("odebral gracz: " + RemnetId + " od gracza ");

        //Vector3 positionOf = PersistantSettings.chosenWpn.prefab.transform.localPosition;
        //PersistantSettings.chosenWpn.prefab.transform.parent = transform.GetChild(6);
        //PersistantSettings.chosenWpn.prefab.transform.localPosition = positionOf;
        //localComponents[11] = PersistantSettings.chosenWpn.script;




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

    [Command]
    public void Cmdstrike(int damage)
    {
        Debug.Log("set");
        health -= damage;
        if (health <= 0)
        {
            die();

        }
        else
        {
            healthbar.text = health.ToString();
            RpcHpDisplay(health);
        }

    }
    public void die()
    {
        Debug.Log("gone");
 //       rawModel.parent = null;
        Destroy(gameObject);

    }
    [ClientRpc]
    public void RpcHpDisplay(int value)
    {
        healthbar.text = value.ToString();
    }









}
