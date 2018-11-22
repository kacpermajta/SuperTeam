using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class generalMultiplayer : NetworkBehaviour
{
    [SerializeField]
    public static Dictionary <int,  multiSetup> PlayerList;
    public static NetworkStartPosition[] spawnPoints;
    [SerializeField]
    public Text[] scoreboardNS;
    public static Text[] scoreboard;
    public static int[] scores;
    // Use this for initialization
    void Start () {
        scoreboard = scoreboardNS;
        PlayerList = new Dictionary<int, multiSetup>();
        spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        //if (!isServer)
        //    this.enabled = false;
        scores =new int[] { 0, 0, 0, 0 };
        foreach (Text score in scoreboard)
        {
            score.text = "0";
        }
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
