using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events; // This is so that you can extend the pointer handlers
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// do not forget that this class is made for clear distinguish of 
// ..two objects, this circumstances affect the performance in a 
// ..bad way, both classes can be merged to increase performance

public class PlayerMovemement : MonoBehaviour
{
    MazeMovement mazeMovement;
    [SerializeField] GameObject maze;

    public SwipeDetection swipeDetection;
    public SceneLoader sceneLoader;
    //public TouchInput touchInput;

    [SerializeField]
    private float rotSpeed;
    [SerializeField]
    private float rotSpeed_f;
    private float rotSpeed2;

    // to determine text of the panel after the game finishes
    public GameObject panel;
    public Text txt;
    
    // to determine text of top-right button (either "?" or "II")
    public GameObject panelPause;
    public Text txtP;

    // to pop-up level selection panel
    public GameObject levelSelection;

    public GameObject playButton;
    public GameObject playNextButton;

    private bool a = false;
    private int l; // variable to determine index of a scene
    public Text txtL; // variable to show level on top-left screen
    private int activeScene; // to determine level buttons

    [HideInInspector] public bool d = false; // to stop the action of the ball if it hits the wall or background

    public Rigidbody rb;
    private Transform levelCanvas;
    private Transform levels;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mazeMovement = maze.GetComponent<MazeMovement>();
        l = SceneManager.GetActiveScene().buildIndex;
        txtL.text = (l+1).ToString(); // this will adjust the level of the game on top-left
        //..by adding +1 to the index 
    }

    void Update()
    { 
        if (Input.GetMouseButtonDown(0))
            rb.useGravity = true; // enable gravity before start

        if (mazeMovement.lastPosition.z > mazeMovement.laneDistance && !d) // go until last position exceeds lane distance
        {
            transform.Rotate(rotSpeed_f * Time.deltaTime, 0f, 0f, Space.World);
            a = true;
        }
        else if (a == true && d == false && swipeDetection.directionChange)        
            transform.Rotate(0f, 0f, mazeMovement.rotSpeed1 * -rotSpeed * Time.deltaTime, Space.World);
        else if (a == true && d == false && !swipeDetection.directionChange)        
            transform.Rotate(0f, 0f, mazeMovement.rotSpeed1 * rotSpeed * Time.deltaTime, Space.World);        
    }

    void OnCollisionEnter(Collision collision)
    {
        // gameobject is not identified in this domain!
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Background")
        {
            d = true;
            panel.SetActive(true);
            levelSelection.SetActive(true);
            levelCanvas = levelSelection.transform.GetChild(0);
            activeScene = PlayerPrefs.GetInt("ActiveScene");
            for (int i = 1; i < activeScene + 2; i++) // make the l saved variable that reaches till the last level
            {
                levels = levelCanvas.transform.GetChild(i);
                levels.gameObject.SetActive(true);
                // levels is a transform that attached to the game object, above line shows how to reach it
            }
        }

        if (collision.gameObject.tag == "Wall")
        {           
            //Explode();
            gameObject.SetActive(false);
            playButton.SetActive(true); // make sure that when it reset the game, the level is correct
            
            txt.text = "YOU LOSE.";
            txt.color = Color.red;
            txtP.text = "?";
        }
        else if (collision.gameObject.tag == "Background")
        {
            sceneLoader.SaveScene();
            if (l != 4) // since level 5 is the last level, i removed the next level button               
                playNextButton.SetActive(true);
            else
                playButton.SetActive(true); // make the game restart the last level
            
            txt.text = "YOU WIN!";
            txt.color = Color.cyan;
            txtP.text = "?";
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
