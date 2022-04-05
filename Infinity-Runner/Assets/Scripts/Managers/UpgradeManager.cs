using System;
using System.Collections;
using System.Collections.Generic;
using InfinityRunner.Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InfinityRunner.Managers
{


    public class UpgradeManager : MonoBehaviour
    {
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

        private void Awake() {
            
            BackUpgradeBtn.onClick.AddListener(Hide);
            
            UpgradeOneBtn.onClick.AddListener(() => UpdateItemDescription(LifeDescription));
            UpgradeTwoBtn.onClick.AddListener(() => UpdateItemDescription(SpeedDescription));
            UpgradeThreeBtn.onClick.AddListener(() => UpdateItemDescription(JumpDescription));
        }

        public void Show() {
            UpgradePanel.SetActive(true);
        }

        private void Hide() {
            UpgradePanel.SetActive(false);
        }
        
        private void UpdateItemDescription(ItemDescription item) {
            DescriptionText.text = item.Description;
            PriceText.text = item.ItemPrice.ToString();
        }
    }
}