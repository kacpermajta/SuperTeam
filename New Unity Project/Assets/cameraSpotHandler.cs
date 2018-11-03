using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSpotHandler : MonoBehaviour {

    public input thisInput;
    public float transitionDuration = 0.2f;
    public Transform spotOrig, spotR, spotL;
    bool movingR, movingL, movingOrig, atOrig, atR;
    // Use this for initialization
    void Start () {
        movingR = false;
        movingOrig = false;
 //       atOrig = true;
        atR = false;
	}

    // Update is called once per frame
    void Update()
    {

        if (thisInput.aimR && atR)
            transform.Rotate(0, thisInput.prevX, 0);


        if (thisInput.aimR && !movingR && !atR)

        {
            StartCoroutine(Transition(spotR));
            movingR = true;
//            atOrig = false;
            
        }
        
        else if (!thisInput.aimR)

        {
            if (atR)
            {
                transform.position = spotOrig.position;
                transform.localRotation = Quaternion.Euler(16.375f, 0, 0);
 //               atOrig = true;
                atR = false;
            }

        }

    }

    IEnumerator Transition(Transform target)
    {
        float t = 0.0f;
        Vector3 startingPos = transform.position;
        while (t < 1.0f)
        {
            t += Time.deltaTime  / transitionDuration;


            transform.position = Vector3.Lerp(startingPos, target.position, t);
            yield  return 0;
        }
        movingR = false;
        movingOrig = false;
        atR = true;
    }
}
