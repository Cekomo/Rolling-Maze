using System.Collections;
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
    private  static bool wasInternetConnected;

    [SerializeField] private string androidGameId;
    [SerializeField] private string interstitialPlacementId;
    [SerializeField] private string rewardedPlacementId;
    [SerializeField] private bool inTestMode;

    private void Start()
    {
        StartCoroutine(CheckInternetAtStart());
        StartCoroutine(CheckInternetRegularly());
    }

    private void LoadAd(string placementId)
    {
        // Debug.Log("Showing Ad: " + placementId);
        Advertisement.Load(placementId, this);
    }
    
    private void TryShowingInterstitialAd()
    {
        // Debug.Log("Showing Ad: " + interstitialPlacementId);
        if (IsInternetReachable()) // there was a problem at first where this is triggered during initialization fail
        {
            GameManager.IsAdsActive = true;
            Advertisement.Show(interstitialPlacementId, this);
            AdShowingCounter = 0;
        }
        else
        {
            GameManager.IsAdsActive = false;    
            if (GameManager.IsLevelCompleted) return;
            
            MazeMovementController.ResetRotationBehavior(); 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
    }
    
    public void TryShowingRewardedAd()
    {
        // Debug.Log("Showing Ad: " + rewardedPlacementId);
        if (IsInternetReachable())
        {
            showAdButton.interactable = false;
            Advertisement.Show(rewardedPlacementId, this);
        }
        else 
        {
            IsAdShowable = false;
            showAdButton.interactable = false;
        }
    }
    
    public void ShowAdInEvery3Attempt()
    {
        AdShowingCounter++;
        if (AdShowingCounter < 3) return;
        
        TryShowingInterstitialAd();
    }

    private void ImplementAd()
    {
        Advertisement.Initialize(androidGameId, inTestMode, this);
        LoadAd(interstitialPlacementId);
        LoadAd(rewardedPlacementId);
    }
    
    private IEnumerator CheckInternetRegularly()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);

            if ((!wasInternetConnected && IsInternetReachable()) || !IsAdShowable)
            {
                ImplementAd();
            }
            
            wasInternetConnected = IsInternetReachable();
        }
    }

    private IEnumerator CheckInternetAtStart()
    {
        yield return null;

        if (IsInternetReachable())
        {
            ImplementAd();
        }

        wasInternetConnected = IsInternetReachable();
    }
    
    public static bool IsInternetReachable()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
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
            // LoadAd(rewardedPlacementId);
            
            if (showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
                GameManager.IncreaseTotalGoldByFactor(3);
            UIManager.IsBonusReadyToPop = true; 

            uIManager.CloseEndPanel();
            uIManager.GoNextLevel();
        }
        else if (adUnitId.Equals(interstitialPlacementId))
        {
            // LoadAd(interstitialPlacementId);
            
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
            GameManager.IsAdsActive = false;
            if (GameManager.IsLevelCompleted) return;
            
            MazeMovementController.ResetRotationBehavior(); 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
        else if (adUnitId.Equals(rewardedPlacementId))
            IsAdShowable = false;
    }

    public void OnUnityAdsShowStart(string adUnitId)
    {
        GameManager.IsAdsActive = true;
    }
    
    public void OnUnityAdsShowClick(string adUnitId) { }
    
    private void OnDestroy()
    {
        // Clean up the button listeners:
        showAdButton.onClick.RemoveAllListeners();
    }
}