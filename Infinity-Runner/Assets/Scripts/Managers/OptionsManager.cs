using System;
using System.Collections;
using System.Collections.Generic;
using InfinityRunner.Save;
using InfinityRunner.Scriptables;
using InfinityRunner.Utils;
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
        public GameObject AudioControllerObject;

        public AudioClip ClickButton;
        public List<GameObject> ObjectsToHideForCredits;
        public GameObject CreditsText;
        public GameObject CreditsContainer;
        
        
        private readonly Dictionary<int, Vector2Int> m_resolutionsInfo = new Dictionary<int, Vector2Int>();
        private bool m_isCreditsScrolling;
        
        private void Start() {
            Resolution.onValueChanged.AddListener(delegate {
                OnResolutionUpdate(Resolution);
            });
            
            VfxVolumeSlider.onValueChanged.AddListener(delegate {
                GameSettingsData.VfxVolume = VfxVolumeSlider.value;
            });
            
            MusicVolumeSlider.onValueChanged.AddListener(delegate {
                var audioSource = AudioControllerObject.GetComponent<AudioSource>();
                audioSource.volume = MusicVolumeSlider.value;
                GameSettingsData.MusicVolume = MusicVolumeSlider.value;
            });
            
            FullscreenToggle.onValueChanged.AddListener(delegate {
                GameSettingsData.IsFullscreen = FullscreenToggle.isOn;
            });
            
            m_resolutionsInfo[0] = new Vector2Int(1920, 1080);
            m_resolutionsInfo[1] = new Vector2Int(1280, 720);
            
            BackBtn.onClick.AddListener(Hide);
            
            CredtisBtn.onClick.AddListener(() => {
                EnableCredits(true);
            });
            
            LoadGameSettings();
        }

        public void Show() {
            OptionsPanel.SetActive(true);
        }
        
        private void Hide() {
            if (m_isCreditsScrolling) {
                EnableCredits(false);
                AudioController.Instance.Play(ClickButton, AudioController.SoundType.SoundEffect2D, GameSettingsData.VfxVolume);
                return;
            }
            
            SaveSystem.SaveGameSettings(GameSettingsData);
            AudioController.Instance.Play(ClickButton, AudioController.SoundType.SoundEffect2D, GameSettingsData.VfxVolume);
            OptionsPanel.SetActive(false);
        }

        private void EnableCredits(bool enable) {
            m_isCreditsScrolling = enable;
            CreditsText.SetActive(enable);
            foreach (var objectToHide in ObjectsToHideForCredits){
                objectToHide.SetActive(!enable);
            }
            CreditsContainer.SetActive(enable); 
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
            if (settings == null) {
                GameSettingsData.VfxVolume = 0.5f;
                GameSettingsData.MusicVolume = 0.2f;
                VfxVolumeSlider.value = GameSettingsData.VfxVolume;
                MusicVolumeSlider.value = GameSettingsData.MusicVolume;
                return;
            }

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