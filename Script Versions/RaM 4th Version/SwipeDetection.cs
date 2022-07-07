using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for now, direction change and ball forward movement work together
// try to seperate them so that, while swiping serves for direction change, 
// ..continuous touch serves ball forward movement

public class SwipeDetection : MonoBehaviour
{

    private Vector3 startPos;
    private int pixelDistToDetect = 250; // it was public
    [HideInInspector] public bool fingerDown;
    [HideInInspector] public bool directionBool;

    [HideInInspector] public bool directionChange = false;
    //[HideInInspector] public bool touchPress;
    [HideInInspector] public bool isForward = false;

    // I do not want direction change during maze movement but could not fix it
    void Update()
    {
        // FOR MOBILE GAME

        //if (fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        //{
        //    startPos = Input.touches[0].position;
        //    fingerDown = true;
        //}

        //if (fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        //{
        //    fingerDown = false;
        //}

        //if (fingerDown)
        //{
        //    if (Input.touches[0].position.x <= startPos.x - pixelDistToDetect)
        //    {
        //        fingerDown = false;
        //        Debug.Log("Swipe Left!");
        //        directionBool = false;
        //    }
        //    else if (Input.touches[0].position.x >= startPos.x + pixelDistToDetect)
        //    {
        //        fingerDown = false;
        //        Debug.Log("Swipe Right!");
        //        directionBool = true;
        //    }
        //}

        //-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_
        // FOR TESTING PC

        if (fingerDown == false && Input.GetMouseButtonDown(0))
        {
            //print("first = " + (Input.mousePosition.x - startPos.x).ToString());
            //print("second = " + (Input.mousePosition.x + startPos.x).ToString());
            startPos = Input.mousePosition;
            fingerDown = true;
        }
                  
        if (fingerDown)
        {
            //print(Input.mousePosition.x);
            //print(startPos.x);
            //print(isForward);

            if (Input.mousePosition.x <= startPos.x - pixelDistToDetect)
            {
                fingerDown = false;
                //Debug.Log("Swipe Left!");
                directionChange = false;
                //print(Input.mousePosition.x);
                //print(startPos.x - pixelDistToDetect);
            }
            else if (Input.mousePosition.x >= startPos.x + pixelDistToDetect)
            {
                fingerDown = false;
                //Debug.Log("Swipe Right!");
                directionChange = true;
                //print(Input.mousePosition.x);
                //print(startPos.x + pixelDistToDetect);
            }
            else if (Input.mousePosition.y >= startPos.x + pixelDistToDetect)
            {
                fingerDown = false;
                isForward = true;
            }
                
                          
        }

        //if (fingerDown && Input.GetMouseButtonUp(0))
        //{
        //    fingerDown = false;
        //}
    }
}
