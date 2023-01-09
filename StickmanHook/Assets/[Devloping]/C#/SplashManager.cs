using System.Collections;
using UnityEngine;
using Admobe.Ads;

public class SplashManager : MonoBehaviour
{
    public GameObject NoInteNetPanel;
    public bool IsInternet = false;

    public static SplashManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    
    void CheckInterNet()
    {
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            NoInteNetPanel.SetActive(true);
            IsInternet = false;
        }
        else
        {
            IsInternet = true;
        }
    }

    public void LoadScene()
    {
        StartCoroutine(LoadNextScene());
    }
    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(4f);

        if(Google.Ads.AppOpenAds.Instance.IsAdAvailable)
        {
            AdmobeController.ShowAppOpen();
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
        }
    }

    public void Tryagain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Splash");
    }
}
