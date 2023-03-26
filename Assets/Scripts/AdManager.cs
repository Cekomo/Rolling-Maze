using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{ // class is initialized for android platform only
    [SerializeField] private UIManager uIManager;
    
    [SerializeField] private Button showAdButton;

    public static int AdShowingCounter;
    public static bool IsAdShowable;

    [SerializeField] private string androidGameId;
    [SerializeField] private string interstitialPlacementId;
    [SerializeField] private string rewardedPlacementId;
    [SerializeField] private bool inTestMode;

    private void Awake()
    {
        Advertisement.Initialize(androidGameId, inTestMode, this);

        if (!IsAdShowable) return;
        LoadAd(interstitialPlacementId);
        LoadAd(rewardedPlacementId);
    }

    private void LoadAd(string placementId)
    {
        // Debug.Log("Showing Ad: " + placementId);
        Advertisement.Load(placementId, this);
    }
    
    private void ShowInterstitialAd()
    {
        // Debug.Log("Showing Ad: " + interstitialPlacementId);
        Advertisement.Show(interstitialPlacementId, this);
    }
    
    private void ShowRewardedAd()
    {
        // Debug.Log("Showing Ad: " + rewardedPlacementId);
        showAdButton.interactable = false;
        Advertisement.Show(rewardedPlacementId, this);
    }
    
    private void TryShowingInterstitialAd()
    {
        ShowInterstitialAd();
    }

    public void ShowAdInEvery3Attempt()
    {
        AdShowingCounter++;
        if (AdShowingCounter < 3) return;
        
        TryShowingInterstitialAd();
        GameManager.IsAdsActive = true;
        AdShowingCounter = 0;
    }

    public void TryShowingRewardedAd()
    {
        ShowRewardedAd();
        GameManager.IsAdsActive = true;
    }

    public void OnInitializationComplete()
    {
        IsAdShowable = true; 
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        IsAdShowable = false;
    }
    
    // Implement Load Listener interface methods: 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Debug.Log("Ad Loaded: " + adUnitId);
    }
    
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        // the problem is rewarded ad triggers one more i.e interstitial triggers than rewarded 
        // on the other hand it should not get triggered
        GameManager.IsAdsActive = false;
      
        if (adUnitId.Equals(rewardedPlacementId))
        {
            if (showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
                GameManager.IncreaseTotalGoldByFactor(3);
            UIManager.IsBonusReadyToPop = true; 

            uIManager.CloseEndPanel();
            uIManager.GoNextLevel();
        }
        else if (adUnitId.Equals(interstitialPlacementId))
        {
            if (GameManager.IsLevelCompleted) return;
            
            MazeMovementController.ResetRotationBehavior(); 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
    }
 
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }
    
    // Implement Show Listener interface methods: 
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        if (adUnitId.Equals(interstitialPlacementId))
        {
            if (GameManager.IsLevelCompleted) return;
            
            GameManager.IsAdsActive = false;
            MazeMovementController.ResetRotationBehavior(); 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
    }
 
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    
    private void OnDestroy()
    {
        // Clean up the button listeners:
        showAdButton.onClick.RemoveAllListeners();
    }
}