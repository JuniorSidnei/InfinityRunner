using System;
using System.Collections;
using System.Collections.Generic;
using InfinityRunner.Scriptables;
using InfinityRunner.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace InfinityRunner.Managers {
    
    public class HubManager : MonoBehaviour {

        [Header("central bts")]
        public Button PlayBtn;
        public Button MenuBtn;
        public Button ShopBtn;
        public Button UpgradeBtn;
        
        public ShopManager ShopManager;
        public UpgradeManager UpgradeManager;
        public AudioClip ClickButton;
        public AudioClip MusicHub;
        public GameSettingsData GameSettingsData;
        
        private void Awake() {
            AudioController.Instance.Stop(AudioController.SoundType.Music);
            AudioController.Instance.Play(MusicHub, AudioController.SoundType.Music, GameSettingsData.MusicVolume, true);
            
            PlayBtn.onClick.AddListener(() => {
                AudioController.Instance.Play(ClickButton, AudioController.SoundType.SoundEffect2D, GameSettingsData.VfxVolume);
                SceneManager.LoadScene("GameScene");
            });

            MenuBtn.onClick.AddListener(() => {
                AudioController.Instance.Play(ClickButton, AudioController.SoundType.SoundEffect2D, GameSettingsData.VfxVolume);
                SceneManager.LoadScene("MainMenu");
            });
            
            ShopBtn.onClick.AddListener(() => {
                AudioController.Instance.Play(ClickButton, AudioController.SoundType.SoundEffect2D, GameSettingsData.VfxVolume);
                ShopManager.Show();
            });
            
            UpgradeBtn.onClick.AddListener(() => {
                AudioController.Instance.Play(ClickButton, AudioController.SoundType.SoundEffect2D, GameSettingsData.VfxVolume);
                UpgradeManager.Show();
            });
        }
    }
}