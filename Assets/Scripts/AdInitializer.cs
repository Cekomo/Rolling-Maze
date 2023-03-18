using UnityEngine;
using UnityEngine.Advertisements;

public class AdInitializer : MonoBehaviour, IUnityAdsInitializationListener
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


    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads initialization failed: {error.ToString()} - {message}");
    }
}
