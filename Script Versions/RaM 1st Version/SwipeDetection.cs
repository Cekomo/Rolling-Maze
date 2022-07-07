using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    //public GameObject maze;
    private Vector3 startPos;
    private int pixelDistToDetect = 300; // it was public
    private bool fingerDown;
    [HideInInspector] public bool directionBool;
    //[HideInInspector] public bool isSwipe = false;

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
            startPos = Input.mousePosition;
            fingerDown = true;
        }

        if (fingerDown)
        {
            if (Input.mousePosition.x <= startPos.x - pixelDistToDetect)
            {
                fingerDown = false;
                //Debug.Log("Swipe Left!");
                directionBool = false;
                //print(Input.mousePosition.x);
                //print(startPos.x - pixelDistToDetect);
            }
            else if (Input.mousePosition.x >= startPos.x + pixelDistToDetect)
            {
                fingerDown = false;
                //Debug.Log("Swipe Right!");
                directionBool = true;
                //print(Input.mousePosition.x);
                //print(startPos.x + pixelDistToDetect);
            }
        }

        if (fingerDown && Input.GetMouseButtonUp(0))
        {
            fingerDown = false;
        }
    }
}
