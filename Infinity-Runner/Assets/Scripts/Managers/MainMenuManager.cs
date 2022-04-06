using System;
using System.Collections;
using System.Collections.Generic;
using InfinityRunner.Scriptables;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace InfinityRunner.Managers {
    
    public class MainMenuManager : MonoBehaviour {
        
        public Button PlayButton;
        public Button OptionsButton;
        public Button QuitButton;

        public OptionsManager OptionsManager;

        public void Start() {
            
            PlayButton.onClick.AddListener(() => {
                SceneManager.LoadScene("GameScene");
            });
            
            OptionsButton.onClick.AddListener(() => {
                OptionsManager.Show();
            });
            
            QuitButton.onClick.AddListener(Application.Quit);
        }
    }
}