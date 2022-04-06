using System;
using System.Collections;
using System.Collections.Generic;
using InfinityRunner.Save;
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

        public TextMeshProUGUI CoinsText;
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
        public PlayerStatus PlayerStatus;

        public delegate void OnPurchaseUpgrade(SelectedUpgrade upgrade);
        public static event OnPurchaseUpgrade onPurchaseUpgrade;

        private Dictionary<SelectedUpgrade, int> m_upgradesPrice = new Dictionary<SelectedUpgrade, int>();
        
        private void Awake() {
            Upgrade = SelectedUpgrade.None;
            BackUpgradeBtn.onClick.AddListener(Hide);
            BuyUpgradeBtn.interactable = false;
            BuyUpgradeBtn.onClick.AddListener(() => {
                if (PlayerStatus.Coins < m_upgradesPrice[Upgrade]) return;
                
                PlayerStatus.Coins -= m_upgradesPrice[Upgrade];
                UpdateStatusUpgrade(Upgrade);
                Upgrade = SelectedUpgrade.None;
                BuyUpgradeBtn.interactable = false;
                SaveSystem.SavePlayerStatus(PlayerStatus);
                CoinsText.text = PlayerStatus.Coins.ToString();
                DescriptionText.text = "";
                PriceText.text = "";
                onPurchaseUpgrade?.Invoke(Upgrade);
            });
            
            UpgradeOneBtn.onClick.AddListener(() => UpdateItemDescription(LifeDescription, SelectedUpgrade.Life));
            UpgradeTwoBtn.onClick.AddListener(() => UpdateItemDescription(SpeedDescription, SelectedUpgrade.Speed));
            UpgradeThreeBtn.onClick.AddListener(() => UpdateItemDescription(JumpDescription, SelectedUpgrade.Jump));

            m_upgradesPrice[SelectedUpgrade.Life] = LifeDescription.ItemPrice;
            m_upgradesPrice[SelectedUpgrade.Jump] = JumpDescription.ItemPrice;
            m_upgradesPrice[SelectedUpgrade.Speed] = SpeedDescription.ItemPrice;
        }

        public void Show() {
            UpgradePanel.SetActive(true);
            CoinsText.text = PlayerStatus.Coins.ToString();
        }

        private void Hide() {
            UpgradePanel.SetActive(false);
        }
        
        private void UpdateItemDescription(ItemDescription item, SelectedUpgrade upgrade) {
            DescriptionText.text = item.Description;
            PriceText.text = item.ItemPrice.ToString();
            Upgrade = upgrade;
            
            if (PlayerStatus.Coins < m_upgradesPrice[Upgrade]) {
                BuyUpgradeBtn.interactable = false;
                return;
            }
            
            BuyUpgradeBtn.interactable = true;
        }

        private void UpdateStatusUpgrade(SelectedUpgrade upgrade) {
            
            switch (upgrade) {
                case SelectedUpgrade.Life:
                    PlayerStatus.Life += 1;
                    break;
                case SelectedUpgrade.Speed:
                    PlayerStatus.Speed += 0.5f;
                    break;
                case SelectedUpgrade.Jump:
                    PlayerStatus.CoinMultiplier += 1;
                    break;
            }
        }
    }
}