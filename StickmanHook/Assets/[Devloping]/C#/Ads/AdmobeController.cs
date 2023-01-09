using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Ads;
using Google.Ads.Data;

namespace Admobe.Ads
{
    public class AdmobeController : MonoBehaviour
    {
        public string ApiUrl;
        public string SceneName;

        public static System.Action loadNewScene;
        public static System.Action<GameObject> TransferGameobject;

        private void OnEnable()
        {
            DontDestroyOnLoad(this.gameObject);
            loadNewScene += LoadPlayScene;
            TransferGameobject += TransferGo;

        }

        private void Start()
        {
            if (ApiUrl == null || !ApiUrl.StartsWith("http"))
            {
                Debug.LogError("Please provide Api URL");
                return;
            }

            StartCoroutine(GetAdsData.AdsData_API(ApiUrl));
        }

        public static void ApiDataGot()
        {
            SplashManager.Instance.LoadScene();
        }
        public static void ShowBanner(bool IsDestroy)
        {
            #region banner Ad
            if (GetAdsData.IsAdsOn)
            {
                if (IsDestroy)
                {
                    BannerAds.Instance.DestroyBanner();
                    BannerAds.Instance.DestroyBanner();
                }
                else
                {
                    BannerAds.Instance.ShowBanner();
                }
            }
            #endregion
        }

        public static void Showinterstitial()
        {
            #region Interstitial Ads
            if (GetAdsData.IsAdsOn)
            {
                if (GetAdsData.counter == 0)
                {
                    GetAdsData.lastloadedCount = GetAdsData.counter;
                    InterAds.Instance.ShowInterAd();

                }
                else if (GetAdsData.lastloadedCount + (GetAdsData.APICounter + 1) == GetAdsData.counter)
                {
                    GetAdsData.lastloadedCount = GetAdsData.counter;
                    InterAds.Instance.ShowInterAd();
                }
                else
                {

                }
                GetAdsData.counter++;
            }
            #endregion
        }

        public static void ShowRewarded()
        {
            #region  Rewareded Ads
            if (GetAdsData.IsAdsOn)
                RewardAds.Instance.ShowRewarded();

            #endregion
        }

        public static void ShowAppOpen()
        {
            #region AppOpen Ad


            if (GetAdsData.IsAdsOn)
                AppOpenAds.Instance.ShowAdIfAvailable();

            #endregion
            
        }

        public void LoadPlayScene()
        {
            StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene()
        {
            yield return new WaitForSeconds(1f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
        }

        private void OnApplicationPause(bool pause)
        {
            if (!pause)
            {
                if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "GamePlay")
                {
                    if(StaticData.IsAdShown == false)
                    {
                      //  ShowAppOpen();
                    }
                }
                else
                {
                    ShowAppOpen();

                }
            }
        }

        public void TransferGo(GameObject obj)
        {
            obj = gameObject;
        }

        private void OnDisable()
        {
            loadNewScene -= LoadPlayScene;
            TransferGameobject -= TransferGo;
        }

    }
}
