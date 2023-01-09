using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dax
{

    public class SoundManager : MonoBehaviour
    {
        public AudioSource Sound;
        public AudioSource BGSource;

        public AudioClip ButtonClick;
        public AudioClip Win;
        public AudioClip Loss;
        public AudioClip WinEnter;
        public AudioClip Jump;


        [SerializeField] Image On;
        [SerializeField] Image Off;
        private bool IsOn = false;

        public static SoundManager instance;


        private void Awake() { instance = this; }

        void Start()
        {

            if (!PlayerPrefs.HasKey("mute"))
            {
                PlayerPrefs.SetInt("mute", 0);
                Load();
            }
            else
            {
                Load();
            }
            ChangrIcon();

            Sound.mute = IsOn;
            BGSource.mute = IsOn;
        }

        public void ButtonClickSound()
        {
            Sound.PlayOneShot(ButtonClick);
        }

        public void OnButtobPress()
        {
            ButtonClickSound();
            if (IsOn == false)
            {
                IsOn = true;
                Sound.mute = true;
                BGSource.mute = true;
            }
            else
            {
                IsOn = false;
                Sound.mute = false;
                BGSource.mute = false;
            }
            Save();
            ChangrIcon();
            Admobe.Ads.AdmobeController.Showinterstitial();
        }

        private void ChangrIcon()
        {
            if (IsOn == false)
            {
                On.enabled = true;
                Off.enabled = false;
            }
            else
            {
                On.enabled = false;
                Off.enabled = true;
            }
        }

        private void Load()
        {
            IsOn = PlayerPrefs.GetInt("mute") == 1;
        }
        private void Save()
        {
            PlayerPrefs.SetInt("mute", IsOn ? 1 : 0);
        }

    }
}
