using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    public static int AdShowingIndex;
    // [SerializeField] private AdInitializer adInitializer;

    public static void ShowAdAfterLevelCompletion()
    {
        AdShowingIndex++;
        if (AdShowingIndex < 3) return; // check this
        
        print("Show ad.");
        AdShowingIndex = 0;
    }
    
    public void WatchRewardedApp()
    {
        print("Rewarded ad initialized.");
        // AdManager.Instance.
    }
}
