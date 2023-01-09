using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dax
{

    public class LevelManager : MonoBehaviour
    {
        [SerializeField] GameObject[] Levels;
        [SerializeField] Button[] Level_Btn;
        [SerializeField] Sprite lockLevelBtnIMG, unlockLevelBtnIMG;
        [SerializeField] TMPro.TextMeshProUGUI GamePlayLevelNo;
        public GameObject GenratedLevelParent;
        public GameObject Level_Gen;
        public int LevelIndex;

        public static LevelManager instance;

        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            Lock_Set();
        }

        public void Lock_Set()
        {
            for (int i = 0; i < Level_Btn.Length; i++)
            {
                if (i <= StaticData.LevelNo)
                {
                    Level_Btn[i].interactable = true;
                    Level_Btn[i].GetComponent<Image>().sprite = unlockLevelBtnIMG;
                    Level_Btn[i].transform.GetChild(0).gameObject.SetActive(true);
                    Level_Btn[i].transform.GetChild(1).gameObject.SetActive(false);
                }
                else
                {
                    Level_Btn[i].interactable = false;
                    Level_Btn[i].GetComponent<Image>().sprite = lockLevelBtnIMG;
                    Level_Btn[i].transform.GetChild(0).gameObject.SetActive(false);
                    Level_Btn[i].transform.GetChild(1).gameObject.SetActive(true);
                }
            }
        }

        public void Level_Btn_Click(int a)
        {
            Admobe.Ads.AdmobeController.Showinterstitial();
            UIManager.instance.ClearAll();
            Level_Genrate(a);
        }


        public void Level_Genrate(int a)
        {
            //SoundManager.instance.ButtonClickSound();
            StaticData.CurLevelNo = a;
            if (Level_Gen != null) Destroy(Level_Gen);
            Level_Gen = Instantiate(Levels[a]);
            Level_Gen.transform.SetParent(GenratedLevelParent.transform);
            GamePlayLevelNo.text = "level " + (a + 1).ToString();
            Lock_Set();
            //UiManager.instance.OnOffPanel(3);
            LevelIndex = (a + 1);
        }
    }
}