using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for now, direction change and ball forward movement work together
// try to seperate them so that, while swiping serves for direction change, 
// ..continuous touch serves ball forward movement

public class SwipeDetection : MonoBehaviour
{
    MazeMovement mazeMovement;

    private Vector3 startPos;
    private int pixelDistToDetect = 100; // it was public
    [HideInInspector] public bool fingerDown;
    //[HideInInspector] public bool directionBool;

    [HideInInspector] public bool directionChange = false;
    [HideInInspector] public bool isForward = false;

    [HideInInspector] public bool fingerUp;
    [HideInInspector] public bool lol = true;

    [HideInInspector] public int i = 0; // this is used as lane multiplier for distance (+1 for each circumference)
    [HideInInspector] public bool p = false; // used to limit i before the game starts

    [HideInInspector] public float t;
    private float timeTaken = 0f;

    // sometimes the ball does not go forward and with swipe left/right it goes forward
    void Start() {  t = 1f; }
    
   
    void Update()
    {
        // FOR MOBILE GAME
        // rotation right/left does not work

        //if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved) // not sure if this is applicable  
        //    timeTaken += Time.deltaTime;

        if (Input.touchCount > 0)
            foreach (Touch touch in Input.touches)
                if (Input.touchCount > 0 && touch.phase == TouchPhase.Moved) // not sure if this is applicable  
                    timeTaken += Time.deltaTime;

        if (fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        { // I assume that this as buttonDown equivalent
            startPos = Input.touches[0].position;
            fingerDown = true;
        }

        if (fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        { // I assume that this as buttonUp equivalent
            fingerDown = false;
            timeTaken = 0f;
        }

        if (fingerDown)
        {
            if (Input.touches[0].position.x <= startPos.x - pixelDistToDetect)
            {
                if (timeTaken > 0.1f)
                {
                    //Debug.Log("Swipe Left!");
                    fingerDown = false;
                    directionChange = false;
                }
            }
            else if (Input.touches[0].position.x >= startPos.x + pixelDistToDetect)
            {
                if (timeTaken > 0.1f)
                {
                    //Debug.Log("Swipe Right!");
                    fingerDown = false;
                    directionChange = true;
                }
            }
            else if (Input.touches[0].position.y >= startPos.y + pixelDistToDetect)
            {
                if (timeTaken > 0.1f)
                {
                    //Debug.Log("Swipe Up!");
                    fingerDown = false;
                    isForward = true;
                    if (p && t > 0.64f) { i++; t = 0f; } // to control i to not increase more than lane number
                }
            }
        }

        //-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_
        // FOR TESTING PC

        //if (fingerDown == false && Input.GetMouseButtonDown(0))
        //    fingerDown = true;

        //if (Input.GetMouseButtonDown(0))
        //    startPos = Input.mousePosition;

        //if (Input.GetMouseButton(0))
        //    timeTaken += Time.deltaTime;

        //if (Input.GetMouseButtonUp(0))
        //    timeTaken = 0f;

        //if (fingerDown)
        //{
        //    // **no buttonup function works inside
        //    if (Input.mousePosition.x <= startPos.x - pixelDistToDetect)
        //    {
        //        if (timeTaken > 0.1f)
        //        {
        //            directionChange = false;
        //            fingerDown = false;
        //            // Debug.Log("Swipe Left!");
        //        }
        //    }
        //    else if (Input.mousePosition.x >= startPos.x + pixelDistToDetect)
        //    {
        //        if (timeTaken > 0.1f)
        //        {
        //            directionChange = true;
        //            fingerDown = false;
        //            // Debug.Log("Swipe Right!");
        //        }
        //    }
        //    else if (Input.mousePosition.y >= startPos.y + pixelDistToDetect)
        //    {
        //        if (timeTaken > 0.1f)
        //        {
        //            isForward = true;
        //            fingerDown = false;
        //            if (p && t > 0.64f) { i++; t = 0f; } // to control i to not increase more than lane number
        //            //startPos.x = 10000f;
        //            // Debug.Log("Swipe Up!");
        //        }
        //    }
        //}
    }
}
