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

        [Header("Upgrade panel Btns")]
        public Button BuyUpgradeBtn;
        public Button BackUpgradeBtn;


        [Header("Panels")]
        public GameObject UpgradePanel;
        public GameObject ShopPanel;

        [Header("Upgrade btns")]
        public Button UpgradeOneBtn;
        public Button UpgradeTwoBtn;
        public Button UpgradeThreeBtn;

        [Header("Upgrade texts descriptions")]
        public TextMeshProUGUI DescriptionText;
        public TextMeshProUGUI PriceText;

        [Header("Itens upgrades descriptions")]
        public ItemDescription LifeDescription;
        public ItemDescription JumpDescription;
        public ItemDescription SpeedDescription;
        
        private void Awake() {
            
            PlayBtn.onClick.AddListener(() => {
                SceneManager.LoadScene("GameScene");
            });

            MenuBtn.onClick.AddListener(() => {
                SceneManager.LoadScene("MainMenu");
            });
            
            ShopBtn.onClick.AddListener(() => {
                //show shop panel
            });

            UpgradeBtn.onClick.AddListener(() => {
                UpgradePanel.SetActive(true);
            });
            
            BackUpgradeBtn.onClick.AddListener(() => {
                UpgradePanel.SetActive(false);
            });
            
            UpgradeOneBtn.onClick.AddListener(() => UpdateItemDescription(LifeDescription));
            UpgradeTwoBtn.onClick.AddListener(() => UpdateItemDescription(SpeedDescription));
            UpgradeThreeBtn.onClick.AddListener(() => UpdateItemDescription(JumpDescription));
        }

        private void UpdateItemDescription(ItemDescription item) {
            DescriptionText.text = item.Description;
            PriceText.text = item.ItemPrice.ToString();
        }
    }
}