using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileHandler : MonoBehaviour {

    public Rigidbody thisBody;
    public GameObject hitParticle;
	// Use this for initialization
	void Start () {
        
        thisBody.AddRelativeForce(0, 0, 1000);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerStay(Collider other)

    {
        if (other.tag != "nonexist")
        {
            GameObject hitInstance = Instantiate(hitParticle, transform.position, transform.rotation);
            Destroy(hitInstance, 3);
            try
            {
                Debug.Log("ready");
                other.GetComponent<hitboxManager>().strike(5);

            }
            catch
            {
                Debug.Log("noope");
            }
            Destroy(gameObject);
        }
    }



}
