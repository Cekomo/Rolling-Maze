using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanelGuide;
    [SerializeField] private GameObject startPanelDefault;
    [SerializeField] private TMP_Text levelCount;

    [SerializeField] private GameObject levelEndPanel;

    [SerializeField] private TMP_Text goldCountText;
    [SerializeField] private TMP_Text goldGainText;
    [SerializeField] private TMP_Text skinCostText;

    [SerializeField] private Image[] inventorySlotEdges;
    private int previousSelectedSlot;

    private static bool IsMuted;
    [SerializeField] private Button audioButton;
    private Image _unmuteImage;
    private Image _muteImage;
    
    [SerializeField] private Button storeButton;
    [SerializeField] private GameObject storePanel;
    private Image _storeImage;
    private Image _crossImage;

    private void Awake()
    {
        SetSlotColorAsSelected(PlayerPrefs.GetInt("CurrentSkinIndex"));
        
        _unmuteImage = audioButton.transform.GetChild(0).GetComponent<Image>();
        _muteImage = audioButton.transform.GetChild(1).GetComponent<Image>();
        _storeImage = storeButton.transform.GetChild(0).GetComponent<Image>();
        _crossImage = storeButton.transform.GetChild(1).GetComponent<Image>();
    }

    private void Start()
    {
        SetLevelCounter();
        SetStartPanelStatus(true);
        storeButton.gameObject.SetActive(true);
    }

    private void Update() // transfer this if you create a class like gameManager 
    {
        if (Input.touchCount == 0 || Input.touches[0].position.y > Screen.height * 0.8f || 
            GameManager.IsStoreActive || GameManager.IsEndPanelActive) return;
        
        storeButton.gameObject.SetActive(false);
        LevelLoader.PauseGame(false);
        SetStartPanelStatus(false);
    }
    public void SetLevelCounter()
    {
        levelCount.text = "Level " + (LevelLoader.GetLevel() + 1);
    }
    
    public void SetStartPanelStatus(bool isActive)
    {
        if (LevelLoader.GetLevel() != 0) 
            startPanelDefault.SetActive(isActive);
        else 
            startPanelGuide.SetActive(isActive);
    }
    
    public void ToggleMute()
    {
        IsMuted = !IsMuted;
        AudioListener.pause = IsMuted;

        _unmuteImage.gameObject.SetActive(!IsMuted);
        _muteImage.gameObject.SetActive(IsMuted);
    }

    public void ToggleStore()
    {
        GameManager.IsStoreActive = !GameManager.IsStoreActive;
        
        SetGoldCounter();
        SetSkinCost();
        storePanel.gameObject.SetActive(GameManager.IsStoreActive);
        _storeImage.gameObject.SetActive(!GameManager.IsStoreActive);
        _crossImage.gameObject.SetActive(GameManager.IsStoreActive);
        SetStartPanelStatus(!GameManager.IsStoreActive);
    }

    public void SetSlotColorAsSelected(int slotIndex)
    {
        inventorySlotEdges[previousSelectedSlot].color = MazeModels.ColorDict[Colors.Teal];
        inventorySlotEdges[slotIndex].color = MazeModels.ColorDict[Colors.LightBlue];
        previousSelectedSlot = slotIndex;
    }
    
    public void SetGoldCounter()
    {
        goldCountText.text = PlayerPrefs.GetInt("GamePoint").ToString();
    }

    private void SetLevelGoldGain()
    {
        goldGainText.text = (SkinManager.LevelPoint * (1 + PlayerPrefs.GetInt("PointMultiplier") / 3)).ToString();
    }

    private void SetLevelTries()
    {
        
    }

    public void SetSkinCost()
    {
        skinCostText.text = SkinManager.SelectedSkinValue == 1 ? 
            "EQUIPPED" : SkinManager.BallSkinCosts[SkinManager.SelectedSkinIndex].ToString();
    }

    public void SetLevelEndPanel()
    {
        GameManager.IsEndPanelActive = true;
        levelEndPanel.gameObject.SetActive(true);
        
        SetLevelGoldGain();
        SetLevelTries();
    }

    public void GoNextLevel()
    {
        GameManager.IsEndPanelActive = false;
        levelEndPanel.gameObject.SetActive(false);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        SetLevelCounter();
        SetStartPanelStatus(true);
        
        var theMazeScale = MazeModels.MazeScaleList[LevelLoader.GetLevel()];
        CameraMovementController.PausedOffset = new Vector3(0, theMazeScale * 5, theMazeScale * 4);
    }
}
