using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// wait command can be applied due to the wait drop of the ball
public class MazeMovement : MonoBehaviour
{
    //public SwipeDetection swipeDetection;
    public TouchInput touchInput;

    PlayerMovemement playerMovemement;
    [SerializeField] GameObject player;

    [SerializeField] private float rotSpeed;
    public float linSpeed;   

    public GameObject tPanel;
    public Text tTxt;

    private float distanceTraveled = 0f;
    [HideInInspector] public Vector3 lastPosition;
    [HideInInspector] public float laneDistance = 0f; // distance that the ball should go to reach next lane
    [HideInInspector] public float rotSpeed1 = 1f; // left/right rotational speed of the ball

    private bool a = false; // for starting the rotational movement of the maze
    //private bool b = false; // for changing the direction of the maze (false for auto direction change)
    private bool c = false; // for switching from rotational speed to linear speed
    [HideInInspector] public bool e = false; // for changing the direction of the maze with "a" and "d" keys

    //private float timer = 0f; // timer to count movement time of the maze (one click movement)
    private int ct = 7; // this direction try varies the size of the maze 
    private float t = 0f; // this is for speeding up the left/right rotational speed of the ball
    private float pi = 3.14159f;
    private int i = 0; // this is used as lane multiplier for distance (+1 for each circumference)


    private bool startbool = false; // it enables startime to start counting
    [HideInInspector] public float startime = 0f; // enables to start the game when it reaches 0.6678 seconds

    void Awake()
    {
        playerMovemement = player.GetComponent<PlayerMovemement>();
    }

    void Start()
    {
        lastPosition = transform.position;
        tTxt.text = ct.ToString();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            startbool = true;
        if(startbool)
            startime += Time.deltaTime;

        // Temporary solution until sliding mechanism
        Vector3 p = Input.mousePosition;
        if (p.x >= Screen.width / 2)
            touchInput.touchPress = true;
        else
            touchInput.touchPress = false;
        // ------------------------------------------

        if (Input.GetButtonDown("Fire1") && touchInput.touchPress && startime > 0.6678) // it was GetButton(0)
        {           
            i++;

            a = true;
            c = true;
            
            t += Time.deltaTime;
            //print(t);
 
            transform.Rotate(0f, 0f, 0f); // make zero the rotational speed until
                                          // 0.4 seconds passes           
        }

        if (c == true && playerMovemement.d == false)
        {
            //print("in");
            transform.position += Vector3.forward * -linSpeed * Time.deltaTime;

            // lastPosition gives the distance that the ball passes in z-axis
            distanceTraveled += Vector3.Distance(transform.position, lastPosition);
            lastPosition = transform.position; // distance traveled between to line = 0.85 
            //print(lastPosition.z);
            
            laneDistance = -0.85f * 0.985f * i;
            if (lastPosition.z < laneDistance) // after mouse-click, make zero the rotational speed 
                               // and speed the linear speed up until "0.4" seconds (one click movement)
            {
                c = false;
                touchInput.touchPress = false;
                rotSpeed1 = -2 * pi * lastPosition.z / 10; // kinda slower in small radiues and faster in large radius
                // it seems okay for now, check for greater radius
            }
        }
        
        if (!touchInput.directionChange) // "Input.GetKeyDown("d")" for keyboard control
        {
            
            if (ct > 0 && e == true && playerMovemement.d == false && a) 
            {
                ct--;
                e = false;
            }   if(ct >= 0) { tTxt.text = ct.ToString(); }
        }   
        if (touchInput.directionChange) // "Input.GetKeyDown("a")" for keyboard control
        {
            
            if (ct > 0 && e == false && playerMovemement.d == false && a) 
            {
                ct--;
                e = true;
            }   if (ct >= 0) { tTxt.text = ct.ToString(); }
        }
        
        if (e == false && a == true && c == false && playerMovemement.d == false)       
            transform.Rotate(0f, rotSpeed * Time.deltaTime, 0f); // rotate clockwise for every odd-number mouse click
        
        if (e == true && a == true && c == false && playerMovemement.d == false)        
            transform.Rotate(0f, -rotSpeed * Time.deltaTime, 0f); // rotate clockwise for every even-number mouse click
        

    }

}
