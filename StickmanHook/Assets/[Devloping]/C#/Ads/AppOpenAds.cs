using System;
using UnityEngine;
using GoogleMobileAds.Api;

namespace Google.Ads
{
    public class AppOpenAds 
    {
        private DateTime loadTime;

        public  string AppOpenID;
        private static AppOpenAds instance;

        private AppOpenAd ad;

        public bool isShowingAd = false;
        bool IsFirst = true;

        public static AppOpenAds Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppOpenAds();
                }

                return instance;
            }
        }



        public bool IsAdAvailable
        {
            get
            {
                return ad != null && (System.DateTime.UtcNow - loadTime).TotalHours < 4;

            }
        }

        public void RequestAppOpen()
        {
            AdRequest request = new AdRequest.Builder().Build();

            // Load an app open ad for portrait orientation
            AppOpenAd.LoadAd(AppOpenID, ScreenOrientation.LandscapeRight, request, ((appOpenAd, error) =>
            {
                if (error != null)
                {
                    // Handle the error.
                    Debug.LogFormat("Failed to load the ad. (reason: {0})", error.LoadAdError.GetMessage());
                    return;
                }

                // App open ad is loaded.
                ad = appOpenAd;

                loadTime = DateTime.UtcNow;
            }));
        }


        public void ShowAdIfAvailable()
        {
            if (!IsAdAvailable || isShowingAd )
            {
                return;
            }

            ad.OnAdDidDismissFullScreenContent += HandleAdDidDismissFullScreenContent;
            ad.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresentFullScreenContent;
            ad.OnAdDidPresentFullScreenContent += HandleAdDidPresentFullScreenContent;
            ad.OnAdDidRecordImpression += HandleAdDidRecordImpression;
            ad.OnPaidEvent += HandlePaidEvent;
            ad.Show();
        }

        #region AppOpen CallBacks
        private void HandleAdDidDismissFullScreenContent(object sender, EventArgs args)
        {
            Debug.Log("Closed app open ad");
            // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
            ad = null;
            isShowingAd = false;
            RequestAppOpen();
            
            if(IsFirst)
            {
                IsFirst = false;
                Admobe.Ads.AdmobeController.loadNewScene?.Invoke();
            }
        }

        private void HandleAdFailedToPresentFullScreenContent(object sender, AdErrorEventArgs args)
        {
            Debug.LogFormat("Failed to present the ad (reason: {0})", args.AdError.GetMessage());
            // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
            ad = null;
            RequestAppOpen();
        }

        private void HandleAdDidPresentFullScreenContent(object sender, EventArgs args)
        {
            Debug.Log("Displayed app open ad");
            isShowingAd = true;
        }

        private void HandleAdDidRecordImpression(object sender, EventArgs args)
        {
            Debug.Log("Recorded ad impression");
        }

        private void HandlePaidEvent(object sender, AdValueEventArgs args)
        {
            Debug.LogFormat("Received paid event. (currency: {0}, value: {1}",
                    args.AdValue.CurrencyCode, args.AdValue.Value);
        }
        #endregion
    }
}
