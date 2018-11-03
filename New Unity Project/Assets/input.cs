using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class input : MonoBehaviour {
    public bool front, back, left, right, up, down, aimR, attackR, grab;
    public float turn, rise;
    public float prevX, prevY;
    public int agentType; //0- human 1- dummy 2-turret 3-bot
    float newX, newY;
    float prevTime;
    // Use this for initialization
	void Start () {
        prevX = Input.GetAxisRaw("Mouse X");
        prevY = Input.GetAxisRaw("Mouse Y");
        front = back = left = right = up = down = aimR = attackR = grab = false;
        turn = rise = prevX = prevY = 0f;
    }

    // Update is called once per frame
    void Update () {
        switch (agentType)
        {
            case 1:
                break;
            case 2:
                prevX = 1f;
                attackR = true;
                break;
            case 0:

                newX = Input.GetAxisRaw("Mouse X");
                newY = Input.GetAxisRaw("Mouse Y");
                //turn += (newX - prevX) / Time.deltaTime;
                //rise += (newY - prevY) / Time.deltaTime;


                prevX = newX;
                prevY = newY;
                front = Input.GetKey("w");
                back = Input.GetKey("s");
                left = Input.GetKey("a");
                right = Input.GetKey("d");
                up = Input.GetKey("space");
                grab = Input.GetKey("f");
                aimR = Input.GetKey("e");
                attackR = Input.GetKey(KeyCode.Mouse0);
                break;
        }
    }
}
