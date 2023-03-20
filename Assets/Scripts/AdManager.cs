using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{ // class is initialized for android platform only
    // [SerializeField] private 
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
    }

    private void LoadAd(string placementId)
    {
        Debug.Log("Showing Ad: " + placementId);
        Advertisement.Load(placementId, this);
    }
    
    private void ShowAd(string placementId)
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        Debug.Log("Showing Ad: " + placementId);
        Advertisement.Show(placementId, this);
    }
    
    private void ShowRewardedAd()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        Debug.Log("Showing Ad: " + rewardedPlacementId);
        showAdButton.interactable = false;
        Advertisement.Show(rewardedPlacementId, this);
    }
    
    private void TryShowingInterstitialAd()
    {
        LoadAd(interstitialPlacementId);
        ShowAd(interstitialPlacementId);
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
        LoadAd(rewardedPlacementId);
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
        }
    }
    
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(rewardedPlacementId) && 
            showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.
            
            // Load another ad:
            Advertisement.Load(rewardedPlacementId, this);
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