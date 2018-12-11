using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{

    public void ShowRewardedAd()
    {
        Debug.Log("Showing Rewarded Ad");

        // Check if the advertisement is ready
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions
            {
                resultCallback = HandleShowResult
            };

            Advertisement.Show("rewardedVideo", options);
        }

    }

    void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                // Award 100G to player
                Debug.Log("You finished the ad, here's 100 gems");
                GameManager.Instance.Player.AddGems(100);
                UIManager.Instance.OpenShop(GameManager.Instance.Player.diamonds);
                break;

            case ShowResult.Skipped:
                Debug.Log("Ad skipped no gems awarded");
                break;

            case ShowResult.Failed:
                Debug.Log("Ad failed, no gems awarded");
                break;
        }
    }
}
