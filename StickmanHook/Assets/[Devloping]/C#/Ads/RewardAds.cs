using System;
using UnityEngine;
using GoogleMobileAds.Api;

namespace Google.Ads
{
    public class RewardAds
    {
        private RewardedAd rewardedAd;
        public string RewardeID;
        private static RewardAds instance;

        public static RewardAds Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RewardAds();
                }

                return instance;
            }
        }
        public void RequetsRewarded()
        {
            this.rewardedAd = new RewardedAd(RewardeID);

            this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
            this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
            this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
            this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
            this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

            AdRequest request = new AdRequest.Builder().Build();
            this.rewardedAd.LoadAd(request);
        }

        public void ShowRewarded()
        {
            if(rewardedAd.IsLoaded())
            {
                GameObject Obj = new GameObject();
                Admobe.Ads.AdmobeController.TransferGameobject?.Invoke(Obj);
                Obj.SetActive(false); 

                rewardedAd.Show();
            }
        }

        #region Reward Callbacks
        public void HandleRewardedAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdLoaded event received");
        }

        public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            MonoBehaviour.print(
                "HandleRewardedAdFailedToLoad event received with message: "
                                 + args.LoadAdError);
        }

        public void HandleRewardedAdOpening(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdOpening event received");
            
        }


        public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
        {
            MonoBehaviour.print(
                "HandleRewardedAdFailedToShow event received with message: "
                                 + args.Message);
        }

        public void HandleRewardedAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdClosed event received");
            AppOpenAds.Instance.isShowingAd = false;
            StaticData.IsAdShown = false;
            GameObject Obj = new GameObject();
            Admobe.Ads.AdmobeController.TransferGameobject?.Invoke(Obj);
            Obj.SetActive(true); ;


        }

        public void HandleUserEarnedReward(object sender, Reward args)
        {
            string type = args.Type;
            double amount = args.Amount;
            MonoBehaviour.print(
                "HandleRewardedAdRewarded event received for "
                            + amount.ToString() + " " + type);
        }
        #endregion
    }
}
