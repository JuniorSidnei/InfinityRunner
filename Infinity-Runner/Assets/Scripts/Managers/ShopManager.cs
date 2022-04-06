using System;
using System.Collections;
using System.Collections.Generic;
using InfinityRunner.Save;
using InfinityRunner.Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InfinityRunner.Managers {
    
    public class ShopManager : MonoBehaviour {
        
        public enum SelectedItem {
            None, Shoot, Shield, Respawn, Collector 
        }


        public SelectedItem Item;
        public PlayerStatus PlayerStatus;
        
        [Header("Control Btns")]
        public Button BuyUpgradeBtn;
        public Button BackUpgradeBtn;
        
        public TextMeshProUGUI CoinsText;
        public GameObject ShopPanel;
        
        [Header("Upgrade btns")]
        public Button ItemOneBtn;
        public Button ItemTwoBtn;
        public Button ItemThreeBtn;
        public Button ItemFourBtn;

        [Header("Upgrade texts descriptions")]
        public TextMeshProUGUI DescriptionText;
        public TextMeshProUGUI PriceText;
        
        [Header("Itens upgrades descriptions")]
        public ItemDescription ShootDescription;
        public ItemDescription ShieldDescription;
        public ItemDescription CollectorDescription;
        public ItemDescription RespawnDescription;

        [Header("itens quantity")]
        public TextMeshProUGUI ShootQuantity;
        public TextMeshProUGUI ShieldQuantity;
        public TextMeshProUGUI CollectorQuantity;
        public TextMeshProUGUI RespawnQuantity;
        
        private Dictionary<SelectedItem, int> m_itemsPrice = new Dictionary<SelectedItem, int>();
        
        private void Awake() {
            Item = SelectedItem.None;
            BackUpgradeBtn.onClick.AddListener(Hide);
            BuyUpgradeBtn.interactable = false;
            BuyUpgradeBtn.onClick.AddListener(() => {
                if (PlayerStatus.Coins < m_itemsPrice[Item]) return;
                
                PlayerStatus.Coins -= m_itemsPrice[Item];
                UpdateStatusItem(Item);
                UpdateItemsQuantity();
                Item = SelectedItem.None;
                BuyUpgradeBtn.interactable = false;
                SaveSystem.SavePlayerStatus(PlayerStatus);
                CoinsText.text = PlayerStatus.Coins.ToString();
                DescriptionText.text = "";
                PriceText.text = "";
            });
            
            ItemOneBtn.onClick.AddListener(() => UpdateItemDescription(ShootDescription, SelectedItem.Shoot));
            ItemTwoBtn.onClick.AddListener(() => UpdateItemDescription(CollectorDescription, SelectedItem.Collector));
            ItemThreeBtn.onClick.AddListener(() => UpdateItemDescription(ShieldDescription, SelectedItem.Shield));
            ItemFourBtn.onClick.AddListener(() => UpdateItemDescription(RespawnDescription, SelectedItem.Respawn));
            
            
            m_itemsPrice[SelectedItem.Shoot] = ShootDescription.ItemPrice;
            m_itemsPrice[SelectedItem.Shield] = ShieldDescription.ItemPrice;
            m_itemsPrice[SelectedItem.Respawn] = RespawnDescription.ItemPrice;
            m_itemsPrice[SelectedItem.Collector] = CollectorDescription.ItemPrice;

            UpdateItemsQuantity();
        }

        private void UpdateItemsQuantity() {
            ShootQuantity.text = PlayerStatus.Shoot.ToString();
            ShieldQuantity.text = PlayerStatus.Shield.ToString();
            RespawnQuantity.text = PlayerStatus.Respawn.ToString();
            CollectorQuantity.text = PlayerStatus.Collector.ToString();
        }

        private void UpdateStatusItem(SelectedItem item) {
            switch (item) {
                case SelectedItem.Collector:
                    PlayerStatus.Collector += 1;
                    break;
                case SelectedItem.Shoot:
                    PlayerStatus.Shoot += 1;
                    break;
                case SelectedItem.Shield:
                    PlayerStatus.Shield += 1;
                    break;
                case SelectedItem.Respawn:
                    PlayerStatus.Respawn += 1;
                    break;
            }
        }

        private void UpdateItemDescription(ItemDescription item, SelectedItem selectedItem) {
            DescriptionText.text = item.Description;
            PriceText.text = item.ItemPrice.ToString();
            Item = selectedItem;
            
            if (PlayerStatus.Coins < m_itemsPrice[selectedItem]) {
                BuyUpgradeBtn.interactable = false;
                return;
            }
            
            BuyUpgradeBtn.interactable = true;
        }

        public void Show() {
            ShopPanel.SetActive(true);
            CoinsText.text = PlayerStatus.Coins.ToString();
        }

        private void Hide() {
            ShopPanel.SetActive(false);
        }
    }
}