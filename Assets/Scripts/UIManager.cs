using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanelGuide;
    [SerializeField] private GameObject startPanelDefault;
    [SerializeField] private TMP_Text levelCount;
    
    private static bool IsMuted;
    [SerializeField] private Button audioButton;
    private Image unmuteImage;
    private Image muteImage;

    private void Start()
    {
        SetLevelCounter();
        SetStartPanelStatus(true);
        
        unmuteImage = audioButton.transform.GetChild(0).GetComponent<Image>();
        muteImage = audioButton.transform.GetChild(1).GetComponent<Image>();
    }

    private void Update() // transfer this if you create a class like gameManager 
    {
        if (Input.touchCount == 0) return;
        
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
}
