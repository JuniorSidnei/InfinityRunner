using System;
using System.Collections;
using System.Collections.Generic;
using InfinityRunner.Scriptables;
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

        private void Awake() {
            
            PlayBtn.onClick.AddListener(() => {
                SceneManager.LoadScene("GameScene");
            });

            MenuBtn.onClick.AddListener(() => {
                SceneManager.LoadScene("MainMenu");
            });
            
            ShopBtn.onClick.AddListener(() => {
                ShopManager.Show();
            });
            
            UpgradeBtn.onClick.AddListener(() => {
                UpgradeManager.Show();
            });
        }
    }
}