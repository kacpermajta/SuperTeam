using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponMovement : MonoBehaviour {
    public enum wpnType {rifle, pistol, sword, punch, ray, flame};
    public wpnType type;
    public input thisInput;
    public multiSetup thisMulti;
    public GameObject effect;
    public int ammo;
//    public ParticleSystem gunfire;
    public int cooldown, repeatable,damage;
 //   ParticleSystem.EmissionModule emiter;
    bool prevR, prewStrike;
    // Use this for initialization
    void Start () {
 //       emiter = gunfire.emission;
        cooldown = 0;
        prevR = false;
        Physics.IgnoreLayerCollision(8,10);
        
    }
	
	// Update is called once per frame
	void Update () {
        if (thisMulti == null)
            return;
        if (thisInput.aimR)
        {
            transform.Rotate(0, 1.5f*thisInput.prevX, 0);


        }
        else
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        switch (type)
        {
            case wpnType.ray:
                if (prevR != thisInput.attackR)
                {
                    Debug.Log("current value: " + thisInput.attackR + ", prev: " + prevR);
                    if (thisInput.attackR)
                    {
                        //                       thisMulti.CmdEnableParticle(0, transform.position, transform.rotation);
                        thisMulti.CmdTurn(true);
                    }
                    else
                    {
                        //                       thisMulti.CmdDisableParticle(0, transform.position, transform.rotation);
                        thisMulti.CmdTurn(false);
                    }
                }
                if (cooldown==0 && thisInput.attackR)
                {
                    Ray ray = new Ray(transform.position, transform.forward);
                    RaycastHit stream;
                    
                    if (Physics.Raycast(ray, out stream, 30,9))
                    {
                        if (stream.collider.gameObject.tag != "nonexist")
                        {
                            GameObject hitInstance = Instantiate(effect, stream.point, Quaternion.identity);
                            Destroy(hitInstance, 3);

                            try
                            {

                                stream.collider.gameObject.SendMessage("strike", damage, SendMessageOptions.DontRequireReceiver);
                                cooldown = 1;
                            }
                            catch
                            {

                            }
                        }
                    }
                     
                }
                    break;

            case wpnType.rifle:
                if (thisInput.attackR)
                    if (cooldown == 0)
                    {
//                        Debug.Log("pew");
                        thisMulti.CmdSpawnMissile(ammo, damage, transform.position, transform.rotation);

                        cooldown = repeatable;
                    }
                break;
            case wpnType.pistol:
                if (thisInput.attackR)
                    if (cooldown == 0)
                    {
 //                       Debug.Log("pew");
                        thisMulti.CmdSpawnMissile(ammo, damage, transform.position, transform.rotation);

                        cooldown = repeatable;
                    }
                break;
            case wpnType.sword:
                if (prewStrike == true && (cooldown<=0 || cooldown< repeatable-40))
                {
                    thisMulti.CmdTurn(false);
                    prewStrike = false;
                }


                if (thisInput.attackR)
                    if (cooldown == 0)
                    {
                        //                       Debug.Log("pew");
                        GameObject AOEObject = Instantiate(effect, transform);
                        AOEObject.GetComponent<AOEAttack>().dmg = damage;
                        thisMulti.CmdTurn(true);
                        prewStrike = true;
                        cooldown = repeatable;
                    }
                break;

        }
        prevR = thisInput.attackR;
    }
    void FixedUpdate()
    {
        if (cooldown > 0)
        {
            cooldown--;

        }
        


    }

    public void strike()
    {
        switch (type)
        {
            case wpnType.ray:
                Debug.Log("kula");

                
                Debug.Log("emitujze");

                cooldown = 100;
                break;

            case wpnType.rifle:
                
                break;
    }
    }
}
