using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanelGuide;
    [SerializeField] private GameObject startPanelDefault;
    [SerializeField] private TMP_Text levelCount;

    [SerializeField] private TMP_Text totalGoldCount;
    [SerializeField] private TMP_Text goldGain;
    
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
        
        SetTotalGoldCounter();
        storePanel.gameObject.SetActive(GameManager.IsStoreActive);
        storeImage.gameObject.SetActive(!GameManager.IsStoreActive);
        crossImage.gameObject.SetActive(GameManager.IsStoreActive);
        SetStartPanelStatus(!GameManager.IsStoreActive);
    }

    private void SetTotalGoldCounter()
    {
        totalGoldCount.text = "G: " + PlayerPrefs.GetInt("GamePoint");
    }

    public IEnumerator SetCurrentGain()
    {
        var goldGainTextTransparency = goldGain.color.a;
        var goldGainColor = goldGain.color;
        var goldGainPosition = goldGain.transform.position;
        
        goldGain.text = "+" + SkinManager.LevelPoint * (1 + PlayerPrefs.GetInt("PointMultiplier") / 3) + " G";
        
        yield return new WaitForSeconds(0.2f);
        
        while (goldGainTextTransparency > 0)
        {
            goldGainPosition.y += 2f;
            goldGain.transform.position = goldGainPosition;
            goldGainTextTransparency -= 0.05f;
            goldGainColor.a = goldGainTextTransparency;
            goldGain.color = goldGainColor;
            yield return new WaitForSeconds(0.015f);
        }
    }
}
