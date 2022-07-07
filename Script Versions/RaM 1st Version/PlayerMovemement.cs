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

    //float minForce = 125;
    //float maxForce = 750;
    //float radius = 10;

    [SerializeField]
    private float rotSpeed;
    [SerializeField]
    private float rotSpeed_f;

    public GameObject panel;
    public Text txt;

    private bool a = false;
    //private bool b = false;
    private bool c = false;
    [HideInInspector]
    public bool d = false;

    // private float t = 0;
    private float timer = 0f;
    private float ct = 1f;

    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mazeMovement = maze.GetComponent<MazeMovement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ct += 0.3f;
            
            a = true;
            c = true;

            if (mazeMovement.e == false)
            { 
                rb.AddForce(1f, 0, 0);
            }
            else if (mazeMovement.e == true)
            { 
                rb.AddForce(-1f, 0, 0);
            }
        }

        if (c == true && d == false)
        {
            timer += Time.deltaTime;
            transform.Rotate(rotSpeed_f * Time.deltaTime, 0f, 0f, Space.World);
            
            if (timer >= 0.4f)
            {
                c = false;
                timer = 0f;
            }
        }        

        if (a == true && c == false && d == false && mazeMovement.e == true)
        {
            transform.Rotate(0f, 0f, ct * rotSpeed * Time.deltaTime, Space.World);
        }
        else if (a == true && c == false && d == false && mazeMovement.e == false)
        {
            transform.Rotate(0f, 0f, ct * -rotSpeed * Time.deltaTime, Space.World);
        }
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
            txt.text = "YOU SUCK.";
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
