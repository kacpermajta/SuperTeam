﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistantSettings : MonoBehaviour {

    [SerializeField]
    GameObject[] weaponListNS;

    public static GameObject[] weaponList;
    
    public static wpnClass chosenWpn;
    public static GameObject settingObj;

	// Use this for initialization
	void Start () {
        weaponList = weaponListNS;
        DontDestroyOnLoad (transform.gameObject);
        settingObj = gameObject;

        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Button1()
    {
        chosenWpn = new wpnClass(weaponList[0]);
        chosenWpn.num = 0;
        SceneManager.LoadScene("trainingFacility");
    }
    public void Button2()
    {
        chosenWpn = new wpnClass(weaponList[1]);
        chosenWpn.num = 1;
        SceneManager.LoadScene("trainingFacility");
    }
    public void Button3()
    {
        chosenWpn = new wpnClass(weaponList[2]);
        chosenWpn.num = 2;
        SceneManager.LoadScene("trainingFacility");
    }


}