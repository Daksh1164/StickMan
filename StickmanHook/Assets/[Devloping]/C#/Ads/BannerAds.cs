using System;
using UnityEngine;
using GoogleMobileAds.Api;

namespace Google.Ads
{
    public class BannerAds 
    {
        private BannerView bannerView;
        public string BannerId;
        private static BannerAds instance;

        public static BannerAds Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BannerAds();
                }

                return instance;
            }
        }
        private void RequestBanner()
        {

            this.bannerView = new BannerView(BannerId, AdSize.Banner, AdPosition.Top);

            this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
            this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
            this.bannerView.OnAdOpening += this.HandleOnAdOpened;
            this.bannerView.OnAdClosed += this.HandleOnAdClosed;

            AdRequest request = new AdRequest.Builder().Build();

            this.bannerView.LoadAd(request);
        }

        public void ShowBanner()
        {
            this.RequestBanner();
        }

        public void DestroyBanner()
        {
            this.bannerView.Destroy();
        }

        #region Banner CallBacks

        public void HandleOnAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLoaded event received");
        }

        public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                                + args.LoadAdError.GetMessage());
        }

        public void HandleOnAdOpened(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdOpened event received");
        }

        public void HandleOnAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdClosed event received");
        }

        #endregion
    }


}