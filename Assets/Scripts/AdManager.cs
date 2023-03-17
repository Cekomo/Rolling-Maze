using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    public void ShowRewardedAd()
    {
        if (!Advertisement.IsReady("rewardedVideo")) return;

        var options = new ShowOptions()
        {
            resultCallback = HandleShowResult
        };
            
        Advertisement.Show("rewardedVideo", options);
    }

    public void ShowSkippableAd()
    {
        if (!Advertisement.IsReady()) return;
        
        var options = new ShowOptions()
        {
            resultCallback = HandleShowResult
        };
            
        Advertisement.Show(options);
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                // reward user
                break;
            case ShowResult.Skipped:
                print("Ad skipped, no reward.");
                break;
            case ShowResult.Failed:
                print("Ad watch process failed, no reward.");
                break;
        }
    }
}
