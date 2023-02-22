using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private TMP_Text levelCount;

    private void Start()
    {
        SetLevelCounter();
        SetStartPanelStatus(true);
    }

    private void Update() // transfer this if you create a class like gameManager 
    {
        if (Input.touchCount == 0) return;
        
        LevelLoader.PauseGame(false);
        SetStartPanelStatus(false);
    }

    public void SetStartPanelStatus(bool isActive)
    {
        startPanel.SetActive(isActive);
        // Debug.Log("Setting start panel status to: " + isActive);
    }

    public void SetLevelCounter()
    {
        levelCount.text = "Level " + (LevelLoader.GetLevel() + 1);
    }
}
