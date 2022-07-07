using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    public GameObject HowToPanel;
    public GameObject HelpPanel;
    public GameObject Panel;

    //private Text txt;
    //private string text = "?";

    //public Text txtL;
    //private int i = 1;

    public void ResetTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //i++;
        //txtL.text = i.ToString();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OpenPanel()
    {
        if(HowToPanel != null)
        {
            Time.timeScale = 0;

            HowToPanel.SetActive(true);
            Panel.SetActive(false);
            HelpPanel.SetActive(false);
        }
        
    }
    public void OpenPanell()
    {
        if (Panel != null)
        {
            Time.timeScale = 1;

            //txt.text = "?";
            HowToPanel.SetActive(false);
            Panel.SetActive(true);
            HelpPanel.SetActive(true);
        }
    }
}
