using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Admobe.Ads;

namespace Dax
{

    public class UIManager : MonoBehaviour
    {
        public GameObject[] Panels;
        [SerializeField] GameObject Levelparent;
        

        public static UIManager instance;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            OnOffPanels(0);
        }

        public void PauseButton()
        {
            AdmobeController.Showinterstitial();
            Panels[3].SetActive(true);
            StaticData.IsPaused = true;
            Time.timeScale = 0f;
        }


        public void ResumeButton()
        {
            AdmobeController.Showinterstitial();

            Time.timeScale = 1f;
            StaticData.IsPaused = false ;

        }

        public void PlayButton()
        {
            AdmobeController.Showinterstitial();

            Panels[1].SetActive(true);
            Dax.SoundManager.instance.ButtonClickSound();
        }

        public void Restart()
        {
            AdmobeController.Showinterstitial();

            StaticData.IsLoss = false;
            Time.timeScale = 1f;
            Dax.SoundManager.instance.ButtonClickSound();
            Dax.SoundManager.instance.BGSource.Play();
            ClearAll();
            Dax.LevelManager.instance.Level_Genrate(StaticData.CurLevelNo);

        }



        public void HomeButton()
        {
            AdmobeController.Showinterstitial();

            Time.timeScale = 1f;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
            StaticData.IsLoss = false;
            StaticData.IsWin = false;
        }

        public void NextButton()
        {
            AdmobeController.Showinterstitial();


            StaticData.IsWin = false;
            Dax.SoundManager.instance.ButtonClickSound();
            Dax.SoundManager.instance.BGSource.Play();

            Time.timeScale = 1f;
            ClearAll();
            Dax.LevelManager.instance.Level_Genrate(StaticData.CurLevelNo + 1);
        }

        public void OpenGameLossPanel()
        {
            Dax.SoundManager.instance.BGSource.Pause();
            Dax.SoundManager.instance.Sound.PlayOneShot(Dax.SoundManager.instance.Loss);
            StaticData.IsLoss = true;
            Time.timeScale = 0f;
            Panels[4].SetActive(true);
            AdmobeController.Showinterstitial();

        }


        public void OpenGameWinPanel()
        {
            

            StaticData.IsWin = true;
            Dax.SoundManager.instance.BGSource.Pause();
            Dax.SoundManager.instance.Sound.PlayOneShot(Dax.SoundManager.instance.Win);
            Panels[5].SetActive(true);
            Time.timeScale = 0f;

            if (StaticData.LevelNo <= StaticData.CurLevelNo)
            {
                StaticData.LevelNo = StaticData.CurLevelNo + 1;
                Dax.LevelManager.instance.Lock_Set();
            }


            if (StaticData.CurLevelNo == 7)
            {
                StaticData.CurLevelNo = -1;
                Dax.LevelManager.instance.Level_Genrate(StaticData.CurLevelNo);
            }

            AdmobeController.Showinterstitial();

        }


        public void OnOffPanels(int No)
        {
            Dax.SoundManager.instance.ButtonClickSound();
            foreach (var panel in Panels)
            {
                panel.SetActive(false);
            }

            Panels[No].SetActive(true);

        }

        public void ClearAll()
        {
            foreach (Transform child in Levelparent.transform)
            {
                Destroy(child.gameObject);
            }

        }

    }
}
