using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    [SerializeField] private AdsInitializer adsInitializer;
    [SerializeField] private InterstitialAds interstitialAds;
    [SerializeField] private RewardedAds rewardedAds;

    [SerializeField] private string gameId;
    [SerializeField] private string interstitialPlacementId;
    [SerializeField] private string rewardedPlacementId;
    [SerializeField] private bool testMode;

    private void Awake()
    {
        adsInitializer.InitializeAds(gameId, testMode);
    }
    
    
}