using System;
using System.Collections;
using System.Collections.Generic;
using InfinityRunner.Scriptables;
using InfinityRunner.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace InfinityRunner.Managers {
    
    public class MainMenuManager : MonoBehaviour {
        
        public Button PlayButton;
        public Button OptionsButton;
        public Button QuitButton;

        public OptionsManager OptionsManager;

        public AudioClip MenuMusic;
        public AudioClip ClickButton;
        
        public void Start() {
            AudioController.Instance.Play(MenuMusic, AudioController.SoundType.Music, OptionsManager.GameSettingsData.MusicVolume, true);
            PlayButton.onClick.AddListener(() => {
                AudioController.Instance.Play(ClickButton, AudioController.SoundType.SoundEffect2D, OptionsManager.GameSettingsData.VfxVolume);
                SceneManager.LoadScene("GameScene");
            });
            
            OptionsButton.onClick.AddListener(() => {
                AudioController.Instance.Play(ClickButton, AudioController.SoundType.SoundEffect2D, OptionsManager.GameSettingsData.VfxVolume);
                OptionsManager.Show();
            });
            
            QuitButton.onClick.AddListener(() => {
                AudioController.Instance.Play(ClickButton, AudioController.SoundType.SoundEffect2D, OptionsManager.GameSettingsData.VfxVolume);
                Application.Quit();
            });
        }
    }
}