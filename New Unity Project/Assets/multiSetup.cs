using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class multiSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] localComponents;
    [SerializeField]
    graphicCordinator[] weaponGraph;

    public SpriteRenderer teamTag; 

    Camera sceneCamera;

    public GameObject[] missileList;

    GameObject weaponModel;

    public int RemnetId, weaponNum,teamNum;
    int health;
    public Text healthbar;
    bool alive;
    // Use this for initialization
    void Start () {
        RemnetId = int.Parse(GetComponent<NetworkIdentity>().netId.ToString());
        //generalMultiplayer.PlayerList.Add(RemnetId, this);
        if (!isLocalPlayer)
        {
            for (int i = 0; i < localComponents.Length; i++)
            {
                localComponents[i].enabled = false;

            }
        }
        else
        {

            CmdSpawnChar(PersistantSettings.chosenWpn.num,PersistantSettings.teamNum, GetComponent<NetworkIdentity>());

            sceneCamera = Camera.main;
            if (sceneCamera != null)
                sceneCamera.gameObject.SetActive(false);
 

        }
        health = 34;
        healthbar.text = health.ToString();
        alive = true;

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
    private void OnPlayerDisconnected(NetworkPlayer player)
    {
        Debug.Log("bye bye" + RemnetId.ToString());
    }

    [Command]
    public void CmdLeaveServer()
    {
        Debug.Log("bye bye" + RemnetId.ToString());

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
        missileHandler misScript = missile.GetComponent<missileHandler>();
        misScript.dmg = dmg;
        misScript.owner = teamNum;

        NetworkServer.Spawn(missile);


    }
    [Command]

    public void CmdSpawnChar(int wpnum, int teamnum, NetworkIdentity target)
    {
        
        weaponNum = wpnum;
        teamNum = teamnum;
        Debug.Log("nadal gracz: " + RemnetId);
        //        SpawnOtherWpn(RemnetId, num);
        foreach (KeyValuePair<int, multiSetup> sinPlayer in generalMultiplayer.PlayerList)
        {
            sinPlayer.Value.TargetSpawnEachOtherChar(target.connectionToClient, sinPlayer.Value.weaponNum,sinPlayer.Value.teamNum);//
        }
        TargetDisplayScore(target.connectionToClient, generalMultiplayer.scores);
        RpcSpawnOtherChar(wpnum,teamnum);

        generalMultiplayer.PlayerList.Add(RemnetId, this);

    }
    [TargetRpc]
    public void TargetSpawnEachOtherChar(NetworkConnection thisConnection, int targetWpn, int targetTm)
    {
        SpawnOtherWpn(targetWpn);
        SetOtherTeam(targetTm);

    }


    [ClientRpc]
    public void RpcSpawnOtherChar(int wpnum, int tmnum)
    {
        SpawnOtherWpn(wpnum);
        SetOtherTeam(tmnum);


    }


    public void SetOtherTeam(int num)
    {
        teamTag.material = PersistantSettings.teamTags[num];
        teamTag.transform.localPosition = new Vector3(0, -0.989f, 0);
        teamTag.transform.localRotation = Quaternion.Euler(90f, 0, 0);
        return;
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
    public void CmdstrikeById(int damage,int ID)
    {

        generalMultiplayer.PlayerList[ID].Cmdstrike(damage, teamNum);
        

    }
    [ClientRpc]
    public void RpcAddFrag(int team)
    {
        Debug.Log(team);
        generalMultiplayer.scores[team]++;
        generalMultiplayer.scoreboard[team].text = generalMultiplayer.scores[team].ToString();
    }
    [TargetRpc]
    public void TargetDisplayScore(NetworkConnection thisConnection, int[] serverScore)
    {
        generalMultiplayer.scores=serverScore;
        for (int i = 0; i < 4; i++)
        {
            generalMultiplayer.scoreboard[i].text = generalMultiplayer.scores[i].ToString();
        }
    }

    [Command]
    public void Cmdstrike(int damage, int team)
    {
        Debug.Log("set");
        health -= damage;
        if (alive && health <= 0)
        {
            die();
            
            RpcAddFrag(team);
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
        alive = false;
        //       rawModel.parent = null;
        RpcL4D();
        StartCoroutine(Respawn());


    }

    [ClientRpc]
    public void RpcL4D()//called when player killed, disables everything
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        if (isLocalPlayer)
        {
            if (sceneCamera != null)
                sceneCamera.gameObject.SetActive(true);
        }
    }
    [ClientRpc]
    public void RpcRsPlay(Vector3 point)//called when player respawns, reenables everything
    {
        transform.position = point;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);

        }
        if(isLocalPlayer)
        {
            if (sceneCamera != null)
                sceneCamera.gameObject.SetActive(false);
        }
            
    }

    [ClientRpc]
    public void RpcHpDisplay(int value)
    {
        healthbar.text = value.ToString();
    }



    IEnumerator Respawn()
    {
       
        yield return new WaitForSeconds(5);

        // Set the spawn point to origin as a default value
        Vector3 spawnPoint = Vector3.zero;

        // If there is a spawn point array and the array is not empty, pick a spawn point at random
        if (generalMultiplayer.spawnPoints != null && generalMultiplayer.spawnPoints.Length > 0)
        {
            spawnPoint = generalMultiplayer.spawnPoints[Random.Range(0, generalMultiplayer.spawnPoints.Length)].transform.position;
        }

        // Set the player’s position to the chosen spawn point
        transform.position = spawnPoint;



        RpcRsPlay(spawnPoint);
        health = 34;
        alive = true;
        RpcHpDisplay(health);


    }








}
