using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    public input thisInput;
    public multiSetup thisMulti;
 //   public weaponMovement[] weaponScript;
    public collisionCheck grounded;
    public collisionCheck climb;
    public collisionCheck enter;
    public GameObject[] weaponry;
    public Transform rawModel;
    int activeWpn;
    int health;


    Rigidbody targetBody;
    bool holding;
    float turnScale = 0.2f;
    Vector3 appliedForce = new Vector3();
    // Use this for initialization
    void Start()
    {
        targetBody = gameObject.GetComponent<Rigidbody>();
        holding = false;
        activeWpn = 0;
        health = 10;
    }

    // Update is called once per frame
    private void Update()
    {
       if(thisInput.escape)
        {
            PersistantSettings.menu();
            thisMulti.CmdLeaveServer();
        }
    }
    void FixedUpdate()
    {
        if(!thisInput.aimR)
            targetBody.AddRelativeTorque(0f, thisInput.prevX * turnScale, 0f);


        thisInput.turn = 0;
        if (!climb.absvalue && enter.absvalue)
        {
            enter.absvalue = false;
            if (thisInput.front)
            {
                appliedForce.z = 15;
                appliedForce.y = 35;
            }

            else if (thisInput.back)
            {
                appliedForce.z = -15;
                appliedForce.y = 35;
            }
            else
            {
                appliedForce.z = 0;
                appliedForce.y = 0;
            }

        }
        else if (grounded.value)
        {
            grounded.value = false;
            

            if (thisInput.front)
                appliedForce.z = 15;
            else if (thisInput.back)
                appliedForce.z = -15;
            else
                appliedForce.z = 0;

            if (thisInput.left)
                appliedForce.x = -10;
            else if (thisInput.right)
                appliedForce.x = 10;
            else
                appliedForce.x = 0;

            if (thisInput.up)
                appliedForce.y = 400;
            else
                appliedForce.y = 0;
        }

        else if (climb.value && (thisInput.grab || holding))
        {
            climb.value = false;

            if (thisInput.grab)
            {
                if (thisInput.left)
                {
                    appliedForce.x = -10;
                    appliedForce.z = 0;
                }
                else if (thisInput.right)
                {
                    appliedForce.x = 10;
                    appliedForce.z = 0;
                }
                else
                {
                    appliedForce.x = 0;
                    appliedForce.z = 12;
                }


                if (thisInput.up)
                    appliedForce.y = 40;
                else
                    appliedForce.y = 0;

                if (targetBody.velocity.y < 0.1)
                    holding = true;

            }
            else
            {
                appliedForce.z = 0;
                if (holding)
                {



                    holding = false;

                    if (thisInput.up)
                        appliedForce.y = 720;
                    else
                        appliedForce.y = 0;
                    if (thisInput.left)
                        appliedForce.x = -40;
                    else if (thisInput.right)
                        appliedForce.x = 40;
                    else
                        appliedForce.x = 0;

                    if (thisInput.back)
                        appliedForce.y = 1200;

                }
            }



            //if (thisInput.grab&& !holding)
            //{
            //    appliedForce.z = -12;

            //    if (targetBody.velocity.y <0.1)
            //        holding = true;
            //    else
            //        holding = false;
            //}
            //else
            //{

            //    //appliedForce.z = 12;

            //    //if (thisInput.left)
            //    //    appliedForce.x = -10;
            //    //else if (thisInput.right)
            //    //    appliedForce.x = 10;
            //    //else
            //    //    appliedForce.x = 0;

            //    if (holding && thisInput.up)
            //        appliedForce.y = 400;
            //    else
            //        appliedForce.y = 0;
            //}
        }
        
        else
        {
            appliedForce.y = 0;

            if (targetBody.velocity.z < 0.4)
            {
                appliedForce.z = 0;
            }
            if (targetBody.velocity.x < 0.4)
            {
                appliedForce.x = 0;
            }
        }
       
        climb.absvalue = false;
        targetBody.AddRelativeForce(appliedForce);

    }

    public void strike(int damage)
    {
        Debug.Log("set");
        health -= damage;
        if(health<=0)
        {
            die();

        }

    }
    public void die()
    {
        Debug.Log("gone");
        rawModel.parent = null;
        Destroy(gameObject);

    }

}
