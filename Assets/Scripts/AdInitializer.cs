using UnityEngine;
using UnityEngine.Advertisements;

public class AdInitializer : MonoBehaviour, 
    IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private int _androidId = 5208857;
    private string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] private bool testMode;

    private void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        Advertisement.Initialize(_androidAdUnitId, testMode, this);
    }

    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization.
        Debug.Log("Loading Ad: " + _androidAdUnitId);
        Advertisement.Load(_androidAdUnitId, this);
    }
    
    public void ShowRewardedAd(UnityEngine.UI.Button showAdButton)
    {
        // Disable the button:
        showAdButton.interactable = false;
        // Then show the ad:
        Advertisement.Show(_androidAdUnitId, this);
    }
    
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads initialization failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId) { }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) { }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (!adUnitId.Equals(_androidAdUnitId)) return;
        
        switch (showCompletionState)
        {
            case UnityAdsShowCompletionState.COMPLETED:
                // give player reward
                break;
            case UnityAdsShowCompletionState.UNKNOWN:
                Debug.Log("Ad process failed.");
                break;
            case UnityAdsShowCompletionState.SKIPPED:
                Debug.Log("Ad is skipped, no reward given.");
                break;
        }
    }
}
