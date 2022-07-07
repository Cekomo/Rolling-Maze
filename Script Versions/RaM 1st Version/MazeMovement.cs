using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// wait command can be applied due to the wait drop of the ball
public class MazeMovement : MonoBehaviour
{
    [SerializeField]
    private float rotSpeed;
    [SerializeField]
    private float linSpeed;

    public GameObject tPanel;
    public Text tTxt;

    private bool a = false; // for starting the rotational movement of the maze
    //private bool b = false; // for changing the direction of the maze (false for auto direction change)
    private bool c = false; // for switching from rotational speed to linear speed
    [HideInInspector] public bool e = false; // for changing the direction of the maze with "a" and "d" keys

    //private float timer = 0f; // timer to count movement time of the maze (one click movement)
    private int ct = 7; // this direction try varies the size of the maze 

    public SwipeDetection swipeDetection;
    
    PlayerMovemement playerMovemement;
    [SerializeField] GameObject player;

    void Awake()
    {
        playerMovemement = player.GetComponent<PlayerMovemement>();
        //swipeDetection = swiping.GetComponent<SwipeDetection>();
    }

    void Start()
    {
        tTxt.text = ct.ToString();
    }
    
    void Update()
    {

        // should be "Input.GetButtonDown("Fire1")" for one click movement
        if (Input.GetButton("Fire1"))
        {

            a = true;
            c = true;

            // below two line of code help to rotate the maze reversely in every mouse click
            //if (b == false) { b = true; } // change direction of rotational speed
            //else if (b == true) { b = false; } // change direction of rotational speed

            transform.Rotate(0f, 0f, 0f); // make zero the rotational speed until
                                          // 0.4 seconds passes           
        }

        if (c == true && playerMovemement.d == false)
        { 
            //timer += Time.deltaTime; // the time to stop the maze rotation (one click movement)
            transform.position += Vector3.forward * -linSpeed * Time.deltaTime;

            if (Input.GetButtonUp("Fire1")) // after mouse-click, make zero the rotational speed 
                               // and speed the linear speed up until "0.4" seconds (one click movement)
            {
                c = false;
                //timer = 0f; // reset the timer
            }


            /* Below if statement represents the waiting time for maze to linearly move
             * if (timer >= 0.4f) // after mouse-click, make zero the rotational speed 
                               // and speed the linear speed up until "0.4" seconds
            {
                c = false;
                timer = 0f;
            }
            */
        }
        
        if (!swipeDetection.directionBool) // "Input.GetKeyDown("d")" for keyboard control
        {
            
            if (ct > 0 && e == true && playerMovemement.d == false) 
            {
                ct--;
                e = false;
            }
            if(ct >= 0) { tTxt.text = ct.ToString(); }
        }   
        if (swipeDetection.directionBool) // "Input.GetKeyDown("a")" for keyboard control
        {
            
            if (ct > 0 & e == false && playerMovemement.d == false) 
            {
                ct--;
                e = true;
            }
            if (ct >= 0) { tTxt.text = ct.ToString(); }
        }
        
        if (e == false && a == true && c == false && playerMovemement.d == false)
        {
            transform.Rotate(0f, -rotSpeed * Time.deltaTime, 0f); // rotate clockwise for every odd-number mouse click
            //swipeDetection.isSwipe = false;
        }
        else if (e == true && a == true && c == false && playerMovemement.d == false)
        {
            transform.Rotate(0f, rotSpeed * Time.deltaTime, 0f); // rotate clockwise for every even-number mouse click
            //swipeDetection.isSwipe = false;
        }
        //print(swipeDetection.isSwipe);
        /* below if else statements help to rotate the maze reversely in every mouse click
        if (a == true && b == false && c == false && playerMovemement.d == false) 
        {
            transform.Rotate(0f, -rotSpeed * Time.deltaTime, 0f); // rotate clockwise for every odd-number mouse click
        }
        else if (a == true && b == true && c == false && playerMovemement.d == false)
        {
            transform.Rotate(0f, rotSpeed * Time.deltaTime, 0f); // rotate clockwise for every even-number mouse click
        } */
    }

}
