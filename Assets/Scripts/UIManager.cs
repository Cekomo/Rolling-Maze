using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanelGuide;
    [SerializeField] private GameObject startPanelDefault;
    [SerializeField] private TMP_Text levelCount;

    [SerializeField] private TMP_Text goldCount;
    
    private static bool IsMuted;
    [SerializeField] private Button audioButton;
    private Image unmuteImage;
    private Image muteImage;
    
    [SerializeField] private Button storeButton;
    [SerializeField] private GameObject storePanel;
    private Image storeImage;
    private Image crossImage;

    private void Awake()
    {
        unmuteImage = audioButton.transform.GetChild(0).GetComponent<Image>();
        muteImage = audioButton.transform.GetChild(1).GetComponent<Image>();
        storeImage = storeButton.transform.GetChild(0).GetComponent<Image>();
        crossImage = storeButton.transform.GetChild(1).GetComponent<Image>();
    }

    private void Start()
    {
        SetLevelCounter();
        SetStartPanelStatus(true);
        storeButton.gameObject.SetActive(true);
    }

    private void Update() // transfer this if you create a class like gameManager 
    {
        if (Input.touchCount == 0 || Input.touches[0].position.y > Screen.height * 0.8f || GameManager.IsStoreActive) 
            return;
        
        storeButton.gameObject.SetActive(false);
        LevelLoader.PauseGame(false);
        SetStartPanelStatus(false);
    }

    public void SetStartPanelStatus(bool isActive)
    {
        if (LevelLoader.GetLevel() != 0) 
            startPanelDefault.SetActive(isActive);
        else 
            startPanelGuide.SetActive(isActive);
    }

    public void SetLevelCounter()
    {
        levelCount.text = "Level " + (LevelLoader.GetLevel() + 1);
    }

    public void ToggleMute()
    {
        IsMuted = !IsMuted;
        AudioListener.pause = IsMuted;

        unmuteImage.gameObject.SetActive(!IsMuted);
        muteImage.gameObject.SetActive(IsMuted);
    }

    public void ToggleStore()
    {
        GameManager.IsStoreActive = !GameManager.IsStoreActive;
        
        SetCurrentGold();
        storePanel.gameObject.SetActive(GameManager.IsStoreActive);
        storeImage.gameObject.SetActive(!GameManager.IsStoreActive);
        crossImage.gameObject.SetActive(GameManager.IsStoreActive);
        SetStartPanelStatus(!GameManager.IsStoreActive);
    }

    private void SetCurrentGold()
    {
        goldCount.text = "G: " + PlayerPrefs.GetInt("GamePoint");
    }
}
