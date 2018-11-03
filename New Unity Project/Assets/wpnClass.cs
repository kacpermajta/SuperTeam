using System.Collections;
using UnityEngine;

[System.Serializable]
public class wpnClass {

    public GameObject prefab;
    public GameObject CameraSpot;
    public weaponMovement script;
    public graphicCordinator graphics;
    public int num;
    public wpnClass(GameObject prefObject)
    {
        prefab = prefObject;
        /*
        prefab = GameObject.Instantiate(prefObject, PersistantSettings.settingObj.transform, false);
        CameraSpot = prefab.transform.GetChild(0).gameObject;
        script = prefab.GetComponent<weaponMovement>();
        graphics = prefab.GetComponent<graphicCordinator>();
        */
    }

}
