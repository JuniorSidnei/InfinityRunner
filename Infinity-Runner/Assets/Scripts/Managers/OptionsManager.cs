using System;
using System.Collections;
using System.Collections.Generic;
using InfinityRunner.Save;
using InfinityRunner.Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InfinityRunner.Managers {
    
    public class OptionsManager : MonoBehaviour {
        
        public GameObject OptionsPanel;
        public Button CredtisBtn;
        public Button BackBtn;

        public Slider VfxVolumeSlider;
        public Slider MusicVolumeSlider;
        public Toggle FullscreenToggle;
        public TMP_Dropdown Resolution;
        
        public GameSettingsData GameSettingsData;
        
        private readonly Dictionary<int, Vector2Int> m_resolutionsInfo = new Dictionary<int, Vector2Int>();

        private void Start() {
            Resolution.onValueChanged.AddListener(delegate {
                OnResolutionUpdate(Resolution);
            });
            
            VfxVolumeSlider.onValueChanged.AddListener(delegate {
                GameSettingsData.VfxVolume = VfxVolumeSlider.value;
            });
            
            MusicVolumeSlider.onValueChanged.AddListener(delegate {
                GameSettingsData.MusicVolume = MusicVolumeSlider.value;
            });
            
            FullscreenToggle.onValueChanged.AddListener(delegate {
                GameSettingsData.IsFullscreen = FullscreenToggle.isOn;
            });
            
            m_resolutionsInfo[0] = new Vector2Int(1920, 1080);
            m_resolutionsInfo[1] = new Vector2Int(1280, 720);
            
            BackBtn.onClick.AddListener(Hide);
            LoadGameSettings();
        }

        public void Show() {
            OptionsPanel.SetActive(true);
        }
        
        private void Hide() {
            SaveSystem.SaveGameSettings(GameSettingsData);
            OptionsPanel.SetActive(false);
        }

        public void OnChangeFullscreen() {
            Screen.fullScreen = !Screen.fullScreen;
        }

        private void OnResolutionUpdate(TMP_Dropdown dropdown) {
            var resolution = m_resolutionsInfo[dropdown.value];
            GameSettingsData.ResolutionIndex = dropdown.value;
            Screen.SetResolution(resolution.x, resolution.y, Screen.fullScreen);
        }

        private void LoadGameSettings() {
            var settings = SaveSystem.LoadGameSettings();
            if (settings == null) return;

            GameSettingsData.VfxVolume = settings.VfxVolume;
            GameSettingsData.MusicVolume = settings.MusicVolume;
            GameSettingsData.IsFullscreen = settings.IsFullscreen;
            GameSettingsData.ResolutionIndex = settings.ResolutionIndex;

            VfxVolumeSlider.value = GameSettingsData.VfxVolume;
            MusicVolumeSlider.value = GameSettingsData.MusicVolume;
            FullscreenToggle.isOn = GameSettingsData.IsFullscreen;
            Resolution.value = GameSettingsData.ResolutionIndex;
        }
    }
}