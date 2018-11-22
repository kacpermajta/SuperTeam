using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistantSettings : MonoBehaviour {

    [SerializeField]
    GameObject[] weaponListNS;
    public static GameObject[] weaponList;

    [SerializeField]
    Material[] teamTagsNS;
    public static Material[] teamTags;

    public static wpnClass chosenWpn;
    public static GameObject settingObj;
    public static int teamNum;


	// Use this for initialization
	void Start () {
        weaponList = weaponListNS;
        teamTags = teamTagsNS;
        DontDestroyOnLoad (transform.gameObject);
        settingObj = gameObject;
        teamNum = 1;
        
	}
	
	// Update is called once per frame
	void Update () {


		
	}
    public static void menu()
    {
        SceneManager.LoadScene("menu");
    }
    public  void Button1()
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
    public void Button4()
    {
        chosenWpn = new wpnClass(weaponList[3]);
        chosenWpn.num = 3;
        SceneManager.LoadScene("trainingFacility");
    }
    public void Button5()
    {
        chosenWpn = new wpnClass(weaponList[4]);
        chosenWpn.num = 4;
        SceneManager.LoadScene("trainingFacility");
    }
    public void Button6()
    {
        chosenWpn = new wpnClass(weaponList[5]);
        chosenWpn.num = 5;
        SceneManager.LoadScene("trainingFacility");
    }
    public void Button7()
    {
        chosenWpn = new wpnClass(weaponList[6]);
        chosenWpn.num = 6;
        SceneManager.LoadScene("trainingFacility");
    }
    public void GoTeam1()
    {
        teamNum = 0;
    }
    public void GoTeam2()
    {
        teamNum = 1;
    }
    public void GoTeam3()
    {
        teamNum = 2;
    }
    public void GoTeam4()
    {
        teamNum = 3;
    }


}
