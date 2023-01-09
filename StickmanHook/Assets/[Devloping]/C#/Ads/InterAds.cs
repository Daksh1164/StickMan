using System;
using UnityEngine;
using GoogleMobileAds.Api;

namespace Google.Ads
{
    public class InterAds
    {
        private InterstitialAd interstitial;
        public string InterId;
        private static InterAds instance;

        public static InterAds Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InterAds();
                }

                return instance;
            }
        }
        public void RequestInterstitial()
        {
            this.interstitial = new InterstitialAd(InterId);

            this.interstitial.OnAdLoaded += HandleOnAdLoaded;
            this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            this.interstitial.OnAdOpening += HandleOnAdOpening;
            this.interstitial.OnAdClosed += HandleOnAdClosed;

            AdRequest request = new AdRequest.Builder().Build();
            this.interstitial.LoadAd(request);
        }

        public void ShowInterAd()
        {
            if(interstitial.IsLoaded())
            {
                StaticData.IsAdShown = true;
                AppOpenAds.Instance.isShowingAd = true;
                GameObject Obj = new GameObject();
                Admobe.Ads.AdmobeController.TransferGameobject?.Invoke(Obj);
                Obj.SetActive(false); ;
                this.interstitial.Show();


            }
        }

        #region Interstitial Callbacks
        public void HandleOnAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLoaded event received");
        }

        public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                                + args.LoadAdError);
        }

        public void HandleOnAdOpening(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdOpening event received");
            StaticData.IsAdShown = true;
        }

        public void HandleOnAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdClosed event received");
            StaticData.IsAdShown = false;
            GameObject Obj = new GameObject() ;
            Admobe.Ads.AdmobeController.TransferGameobject?.Invoke(Obj);
            Obj.SetActive(true);
            RequestInterstitial();

        }
        #endregion
    }
}