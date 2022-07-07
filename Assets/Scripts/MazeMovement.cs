using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// wait command can be applied due to the wait drop of the ball
public class MazeMovement : MonoBehaviour
{
    public SwipeDetection swipeDetection;
    // public TouchInput touchInput;

    PlayerMovemement playerMovemement;
    [SerializeField] GameObject player;

    [SerializeField] private float rotSpeed;
    public float linSpeed;   

    //public GameObject tPanel;
    //public Text tTxt;

    private float distanceTraveled = 0f;
    [HideInInspector] public Vector3 lastPosition;
    [HideInInspector] public float laneDistance; // distance that the ball should go to reach next lane
    [HideInInspector] public float rotSpeed1 = 1f; // left/right rotational speed of the ball

    private bool a = false; // for starting the rotational movement of the maze
    //private bool b = false; // for changing the direction of the maze (false for auto direction change)
    private bool c = false; // for switching from rotational speed to linear speed
    //[HideInInspector] public bool e = false; // for changing the direction of the maze with "a" and "d" keys

    //private int ct = 9; // this direction try varies the size of the maze 
    private float pi = 3.14159f;

    private bool startbool = false; // it enables startime to start counting
    [HideInInspector] public float startime = 0f; // enables to start the game when it reaches 0.6678 seconds

    void Awake()
    {
        playerMovemement = player.GetComponent<PlayerMovemement>();
    }

    void Start()
    {
        lastPosition = transform.position;
        //tTxt.text = ct.ToString(); // indicator that shows number of tries left in top-left corner
        laneDistance = transform.position.z;
    }
    
    void Update()
    {
        if(startime > 0.6678) // for i to not go beyond single lane distance
            swipeDetection.p = true;

        if (Input.GetMouseButtonDown(0))
            startbool = true;
        if(startbool)
            startime += Time.deltaTime;

        if (Input.GetButton("Fire1") && swipeDetection.isForward && startime > 0.6678) // it was GetButton(0)
        {          
            a = true;
            c = true;

            //transform.Rotate(0f, 0f, 0f); // make zero the rotational speed until
            //                              // 0.4 seconds passes           
        }
        
        if (c == true && playerMovemement.d == false)
        {
            transform.position += Vector3.forward * -linSpeed * Time.deltaTime;

            // lastPosition gives the distance that the ball passes in z-axis
            distanceTraveled += Vector3.Distance(transform.position, lastPosition);
            lastPosition = transform.position; // distance traveled between to line = 0.85 
             
            //print(swipeDetection.i);
            laneDistance = -0.872f * 0.985f * swipeDetection.i; // for first maze it was -.85f
            //t += Time.deltaTime;
            
            swipeDetection.t += Time.deltaTime;

            if (lastPosition.z <= laneDistance) // after mouse-click, make zero the rotational speed 
                               // and speed the linear speed up until "0.4" seconds (one click movement)
            {              
                c = false;
                swipeDetection.isForward = false;
                rotSpeed1 = -2 * pi * lastPosition.z / 10; // kinda slower in small radiues and faster in large radius
                // it seems okay for now, check for greater radius
            }
        }
        
        // Below if statements for limited direction change tries
        //if (!swipeDetection.directionChange) // "Input.GetKeyDown("d")" for keyboard control
        //{
            
        //    if (ct > 0 && e == true && playerMovemement.d == false && a) 
        //    {
        //        ct--;
        //        e = false;
        //    }   if(ct >= 0) { tTxt.text = ct.ToString(); }
        //}   
        //if (swipeDetection.directionChange) // "Input.GetKeyDown("a")" for keyboard control
        //{
            
        //    if (ct > 0 && e == false && playerMovemement.d == false && a) 
        //    {
        //        ct--;
        //        e = true;
        //    }   if (ct >= 0) { tTxt.text = ct.ToString(); }
        //}
        // --------------------------------------------------------

        // delete below if there is no problem
        //if(!swipeDetection.directionChange&& !playerMovemement.d && a)
        //    e = false;
        //else if(swipeDetection.directionChange && !e && !playerMovemement.d && a)
        //    e = true;

        if (!swipeDetection.directionChange && a == true && c == false && playerMovemement.d == false && lastPosition.z < -0.4)       
            transform.Rotate(0f, rotSpeed * Time.deltaTime, 0f); // rotate clockwise for every odd-number mouse click
        
        if (swipeDetection.directionChange && a == true && c == false && playerMovemement.d == false && lastPosition.z < -0.4f)        
            transform.Rotate(0f, -rotSpeed * Time.deltaTime, 0f); // rotate clockwise for every even-number mouse click    
    }
}
