using System;
using System.Collections;
using System.Collections.Generic;
using InfinityRunner.Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InfinityRunner.Managers {

    public class UpgradeManager : MonoBehaviour {

        public enum SelectedUpgrade {
            None, Life, Speed, Jump 
        }
        
        [Header("Control Btns")]
        public Button BuyUpgradeBtn;
        public Button BackUpgradeBtn;
        
        public GameObject UpgradePanel;
        
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

        public SelectedUpgrade Upgrade;

        public delegate void OnPurchaseUpgrade(SelectedUpgrade upgrade);
        public static event OnPurchaseUpgrade onPurchaseUpgrade;
        
        private void Awake() {
            Upgrade = SelectedUpgrade.None;
            BackUpgradeBtn.onClick.AddListener(Hide);
            BuyUpgradeBtn.interactable = false;
            BuyUpgradeBtn.onClick.AddListener(() => {
                onPurchaseUpgrade?.Invoke(Upgrade);
            });
            
            UpgradeOneBtn.onClick.AddListener(() => UpdateItemDescription(LifeDescription, SelectedUpgrade.Life));
            UpgradeTwoBtn.onClick.AddListener(() => UpdateItemDescription(SpeedDescription, SelectedUpgrade.Speed));
            UpgradeThreeBtn.onClick.AddListener(() => UpdateItemDescription(JumpDescription, SelectedUpgrade.Jump));
        }

        public void Show() {
            UpgradePanel.SetActive(true);
        }

        private void Hide() {
            UpgradePanel.SetActive(false);
        }
        
        private void UpdateItemDescription(ItemDescription item, SelectedUpgrade upgrade) {
            DescriptionText.text = item.Description;
            PriceText.text = item.ItemPrice.ToString();
            Upgrade = upgrade;
        }
    }
}