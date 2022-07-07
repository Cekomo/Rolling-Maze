using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events; // This is so that you can extend the pointer handlers
using UnityEngine.EventSystems;

// do not forget that this class is made for clear distinguish of 
// ..two objects, this circumstances affect the performance in a 
// ..bad way, both classes can be merged to increase performance

public class PlayerMovemement : MonoBehaviour
{
    MazeMovement mazeMovement;
    [SerializeField] GameObject maze;

    //public SwipeDetection swipeDetection;
    public TouchInput touchInput;

    //float minForce = 125;
    //float maxForce = 750;
    //float radius = 10;

    [SerializeField]
    private float rotSpeed;
    [SerializeField]
    private float rotSpeed_f;
    private float rotSpeed2;

    public GameObject panel;
    public Text txt;

    private bool a = false;
    //private bool b = false;
    private bool c = false;
    [HideInInspector]
    public bool d = false; // to stop the action of the ball if it hits the wall or background

    private float timer = 0f;

    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mazeMovement = maze.GetComponent<MazeMovement>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            rb.useGravity = true; // enable gravity before start

        if (Input.GetButton("Fire1") && mazeMovement.startime > 0.6678)
        {            

            if (touchInput.touchPress)    
                a = true;
            
            c = true;

            // do not remember why adding those statements (may be important)
            //if (mazeMovement.e == false)            
            //    rb.AddForce(1f, 0, 0);
            //else if (mazeMovement.e == true)            
            //    rb.AddForce(-1f, 0, 0);           
        }

        if (c == true && d == false)
        {
            timer += Time.deltaTime;

            // if(swipeDetection.isForward) // ball rolling foward when pressing
            
            
            if (Input.GetMouseButtonUp(0) || !touchInput.touchPress)
            {
                c = false;
                timer = 0f;
            }
            else //  (touchInput.touchPress)
                transform.Rotate(rotSpeed_f * Time.deltaTime, 0f, 0f, Space.World);
        }        

        if (a == true && c == false && d == false && mazeMovement.e == true)        
            transform.Rotate(0f, 0f, mazeMovement.rotSpeed1 * -rotSpeed * Time.deltaTime, Space.World);
        
        else if (a == true && c == false && d == false && mazeMovement.e == false)        
            transform.Rotate(0f, 0f, mazeMovement.rotSpeed1 * rotSpeed * Time.deltaTime, Space.World);
        
    }

    void OnCollisionEnter(Collision collision)
    {
        // gameobject is not identified in this domain!
        if (collision.gameObject.tag == "Wall")
        {
            d = true;
            //Explode();
            Destroy(gameObject);
            panel.SetActive(true); // it would be nice if timer is added to pop up
            txt.text = "YOU LOSE.";
            txt.color = Color.red;
        }
        else if (collision.gameObject.tag == "Background")
        {
            d = true;
            panel.SetActive(true);
            txt.text = "YOU WIN!";
            txt.color = Color.cyan;
        }
    }

    //private void Explode()
    //{
    //    foreach(Transform t in transform)
    //    {
    //        var rb = t.GetComponent<Rigidbody>();
    //        if (rb != null) 
    //        { 
    //            rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
    //        }
    //    }
    //}
}
