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

    public void ResetTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OpenPanel()
    {
        if(HowToPanel != null)
        {
            HowToPanel.SetActive(true);
            Panel.SetActive(false);
            HelpPanel.SetActive(false);
        }
        
    }
    public void OpenPanell()
    {
    if (Panel != null)
        {
            HowToPanel.SetActive(false);
            Panel.SetActive(true);
            HelpPanel.SetActive(true);
        }
    }
}
