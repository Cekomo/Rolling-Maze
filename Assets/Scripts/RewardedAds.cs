using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
 
public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private Button showAdButton;
    [SerializeField] private string androidAdUnitId = "Rewarded_Android";

    void Awake()
    {
        //Disable the button until the ad is ready to show:
        showAdButton.interactable = false;
    }
 
    // Load content to the Ad Unit:
    public void LoadRewardedAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + androidAdUnitId);
        Advertisement.Load(androidAdUnitId, this);
    }
 
    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
 
        if (adUnitId.Equals(androidAdUnitId))
        {
            // Configure the button to call the ShowAd() method when clicked:
            showAdButton.onClick.AddListener(ShowRewardedAd);
            // Enable the button for users to click:
            showAdButton.interactable = true;
        }
    }
 
    // Implement a method to execute when the user clicks the button:
    public void ShowRewardedAd()
    {
        // Disable the button:
        showAdButton.interactable = false;
        // Then show the ad:
        Advertisement.Show(androidAdUnitId, this);
    }
 
    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(androidAdUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.

            // Load another ad:
            Advertisement.Load(androidAdUnitId, this);
        }
    }
 
    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
 
    private void OnDestroy()
    {
        // Clean up the button listeners:
        showAdButton.onClick.RemoveAllListeners();
    }
}