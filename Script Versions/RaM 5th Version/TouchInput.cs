using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{

    [HideInInspector] public bool directionChange = false;
    [HideInInspector] public bool touchPress;

    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        // Input.GetMouseButtonDown(0)
        {
            Vector3 p = Input.mousePosition; //Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(p);

            if (p.x < Screen.width / 2)
            {
                //touched on the left
                if (!directionChange) { directionChange = true; }
                else { directionChange = false; }
            }
            if (p.x >= Screen.width / 2)
            {
                //touched on the right
                touchPress = true;
            }
        }

        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                Vector3 p = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                Debug.Log(p);

                if (p.x < Screen.width / 2)
                {
                    //touched on the left
                    //do other code
                    Debug.Log("Left Touch");
                    //touchArea = false;
                    touchPress = false;

                    if (!directionChange) { directionChange = true; }
                    else { directionChange = false; }
                }
                else
                {
                    //touched on the right
                    //do other code
                    Debug.Log("Right Touch");
                }
            }


        }
    }
} // erase this if you uncomment below functions

    //void Update()
    //{
    //    if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
    //        RaycastHit hit;

    //        if(Physics.Raycast(ray, out hit))
    //        {
    //            if(hit.collider != null)
    //            {

    //            }
    //        }
    //    }

    //    #if UNITY_EDITOR
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
    //        {
    //            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //            RaycastHit hit;

    //            if (Physics.Raycast(ray, out hit))
    //            {
    //                if (hit.collider != null)
    //                {

    //                }
    //            }
    //        }
    //    #endif
    //    }
//}
