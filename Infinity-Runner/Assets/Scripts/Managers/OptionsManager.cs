using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InfinityRunner.Managers {
    
    public class OptionsManager : MonoBehaviour {
        
        public GameObject OptionsPanel;
        public TMP_Dropdown Resolution;
        public Button CredtisBtn;
        public Button BackBtn;

        private readonly Dictionary<int, Vector2Int> m_resolutionsInfo = new Dictionary<int, Vector2Int>();

        private void Start() {
            Resolution.onValueChanged.AddListener(delegate {
                OnResolutionUpdate(Resolution);
            });
            
            BackBtn.onClick.AddListener(Hide);
            
            m_resolutionsInfo[0] = new Vector2Int(1920, 1080);
            m_resolutionsInfo[1] = new Vector2Int(1280, 720);
        }

        public void Show() {
            OptionsPanel.SetActive(true);
        }
        
        private void Hide() {
            OptionsPanel.SetActive(false);
        }

        public void OnChangeFullscreen() {
            Screen.fullScreen = !Screen.fullScreen;
        }

        private void OnResolutionUpdate(TMP_Dropdown dropdown) {
            var resolution = m_resolutionsInfo[dropdown.value];
            Screen.SetResolution(resolution.x, resolution.y, Screen.fullScreen);
        }
    }
}