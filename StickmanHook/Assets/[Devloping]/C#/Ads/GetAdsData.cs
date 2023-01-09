using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using SimpleJSON;

namespace Google.Ads.Data
{
    public class GetAdsData
    {

        public static bool IsAdsOn;
        public static int APICounter;
        public static int counter = 0;
        public static int lastloadedCount = 0;

        public static IEnumerator AdsData_API(string URl)
        {

            UnityWebRequest request = UnityWebRequest.Post(URl, "");

            yield return request.SendWebRequest();

            if(request.isHttpError || request.isNetworkError)
            {
                Debug.LogError("API Request Falied : " + request.error);
            }
            else
            {

                JSONNode AdsData = JSON.Parse(request.downloadHandler.text);
                Debug.Log($"API Data : { AdsData.ToString()} ");

                BannerAds.Instance.BannerId = AdsData["Banner"].Value;
                InterAds.Instance.InterId = AdsData["Interstitial"].Value;
                RewardAds.Instance.RewardeID = AdsData["Rewarded"].Value;
                AppOpenAds.Instance.AppOpenID = AdsData["AppOpen"].Value;
                IsAdsOn = AdsData["IsAds"].AsBool;
                APICounter = AdsData["Counter"].AsInt;

                InterAds.Instance.RequestInterstitial();
                RewardAds.Instance.RequetsRewarded();
                AppOpenAds.Instance.RequestAppOpen();

                Admobe.Ads.AdmobeController.ApiDataGot();
            }
        }

    }
}
