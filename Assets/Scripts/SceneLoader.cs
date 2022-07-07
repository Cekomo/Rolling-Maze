using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private int activeScene; // to determine and save the index of the scene
    private int temp; // temproray variable to compare if activeScene is greater than new one
    private int loadScene; // variable determined by activeScene to load a scene
    private int currentScene; // current scene for the start to prevent loading scene loop problem
    private int starting = 1;

    //public GameObject background;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        currentScene = SceneManager.GetActiveScene().buildIndex;
        //PlayerPrefs.SetInt("ActiveScene", 0); // to control
        loadScene = PlayerPrefs.GetInt("ActiveScene");
        starting = PlayerPrefs.GetInt("Starting");

        if (loadScene != 0 && currentScene == 0 && starting == 1)       
            SceneManager.LoadScene(loadScene, LoadSceneMode.Single);

        PlayerPrefs.SetInt("Starting", 1);
        PlayerPrefs.Save();

        //print(PlayerPrefs.GetInt("ActiveScene")); // to check current activeScene
    }
    
    public void SaveScene() // it is not saving
    {
        temp = PlayerPrefs.GetInt("ActiveScene");
        activeScene = SceneManager.GetActiveScene().buildIndex;
        activeScene++;
        if(temp < activeScene)
            PlayerPrefs.SetInt("ActiveScene", activeScene);
        //PlayerPrefs.SetInt("ActiveScene", 0); // to control
        PlayerPrefs.Save();
    }

    public void SceneLoad() 
    {
        loadScene = PlayerPrefs.GetInt("ActiveScene");
        SceneManager.LoadScene(loadScene, LoadSceneMode.Single); 
        
        // --Below function may come in handy--
        //DontDestroyOnLoad(background);
    }

    // this requires medium duty load in inspector
    public void Leveller(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
        if(index == 0)
        {
            PlayerPrefs.SetInt("Starting", 0);
            PlayerPrefs.Save();
        }
            
    }
}
