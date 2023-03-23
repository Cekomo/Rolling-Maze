using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{ // class is initialized for android platform only
    [SerializeField] private UIManager uIManager;
    
    [SerializeField] private Button showAdButton;

    public static int AdShowingCounter;

    [SerializeField] private string androidGameId;
    [SerializeField] private string interstitialPlacementId;
    [SerializeField] private string rewardedPlacementId;
    [SerializeField] private bool inTestMode;

    private void Awake()
    {
        // showAdButton.interactable = false;
        Advertisement.Initialize(androidGameId, inTestMode, this);
        LoadAd(interstitialPlacementId);
        LoadAd(rewardedPlacementId);
    }

    private void LoadAd(string placementId)
    {
        Debug.Log("Showing Ad: " + placementId);
        Advertisement.Load(placementId, this);
    }
    
    private void ShowInterstitialAd()
    {
        Debug.Log("Showing Ad: " + interstitialPlacementId);
        Advertisement.Show(interstitialPlacementId, this);
    }
    
    private void ShowRewardedAd()
    {
        Debug.Log("Showing Ad: " + rewardedPlacementId);
        showAdButton.interactable = false;
        Advertisement.Show(rewardedPlacementId, this);
    }
    
    private void TryShowingInterstitialAd()
    {
        GameManager.IsEndPanelActive = true;
        ShowInterstitialAd();
    }

    public void ShowAdInEvery3Attempt()
    {
        AdShowingCounter++;
        if (AdShowingCounter < 3) return;
        
        TryShowingInterstitialAd();
        AdShowingCounter = 0;
    }

    public void TryShowingRewardedAd()
    {
        GameManager.IsEndPanelActive = true;
        ShowRewardedAd();
    }

    public void OnInitializationComplete() { Debug.Log("Unity Ads initialization complete."); }
 
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
    
    // Implement Load Listener interface methods: 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
 
        if (adUnitId.Equals(rewardedPlacementId))
        {
            // Configure the button to call the ShowAd() method when clicked:
            showAdButton.onClick.AddListener(ShowRewardedAd);
            // Enable the button for users to click:
            showAdButton.interactable = true;
            UIManager.IsBonusReadyToPop = true;
        }
    }
    
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        // if (!isAdLoaded) return;

        if (adUnitId.Equals(rewardedPlacementId))
        {
            if (showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
                GameManager.IncreaseTotalGoldByFactor(3);
            
            uIManager.CloseEndPanel();
            uIManager.GoNextLevel();
        }
        else if (adUnitId.Equals(interstitialPlacementId))
        {
            
        }

        if (!uIManager.levelEndPanel.activeSelf) GameManager.IsEndPanelActive = false;
        Advertisement.Load(adUnitId, this);
    }
 
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }
    
    // Implement Show Listener interface methods: 
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }
 
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    
    private void OnDestroy()
    {
        // Clean up the button listeners:
        showAdButton.onClick.RemoveAllListeners();
    }
}