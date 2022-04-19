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
    
    public class ShopManager : MonoBehaviour {
        
        public enum SelectedItem {
            None, Shoot, Shield, Respawn, Collector 
        }


        public SelectedItem Item;
        public PlayerStatus PlayerStatus;
        public GameSettingsData GameSettingsData;
        
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
        
        public AudioClip ClickButton;
        public AudioClip Purchase;
        
        private Dictionary<SelectedItem, int> m_itemsPrice = new Dictionary<SelectedItem, int>();
        
        private void Awake() {
            Item = SelectedItem.None;
            BackUpgradeBtn.onClick.AddListener(Hide);
            BuyUpgradeBtn.interactable = false;
            BuyUpgradeBtn.onClick.AddListener(() => {
                if (PlayerStatus.Coins < m_itemsPrice[Item]) return;
                
                AudioController.Instance.Play(Purchase, AudioController.SoundType.SoundEffect2D, GameSettingsData.VfxVolume);
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
            
           
            ValidateItemObtained(ItemOneBtn, PlayerStatus.Shoot);
            ValidateItemObtained(ItemTwoBtn, PlayerStatus.Collector);
            ValidateItemObtained(ItemThreeBtn, PlayerStatus.Shield);
            ValidateItemObtained(ItemFourBtn, PlayerStatus.Respawn);
            
            
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
            ShootQuantity.text = PlayerStatus.Shoot ? "Obtained!" : ShootDescription.ItemPrice.ToString();
            ShieldQuantity.text = PlayerStatus.Shield ? "Obtained!" : ShieldDescription.ItemPrice.ToString();
            RespawnQuantity.text = PlayerStatus.Respawn ? "Obtained!" : RespawnDescription.ItemPrice.ToString();
            CollectorQuantity.text = PlayerStatus.Collector ? "Obtained!" : CollectorDescription.ItemPrice.ToString();
        }

        private void UpdateStatusItem(SelectedItem item) {
            switch (item) {
                case SelectedItem.Collector:
                    PlayerStatus.Collector = true;
                    break;
                case SelectedItem.Shoot:
                    PlayerStatus.Shoot = true;
                    break;
                case SelectedItem.Shield:
                    PlayerStatus.Shield = true;
                    break;
                case SelectedItem.Respawn:
                    PlayerStatus.Respawn = true;
                    break;
            }
        }

        private void UpdateItemDescription(ItemDescription item, SelectedItem selectedItem) {
            AudioController.Instance.Play(ClickButton, AudioController.SoundType.SoundEffect2D, GameSettingsData.VfxVolume);
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
            AudioController.Instance.Play(ClickButton, AudioController.SoundType.SoundEffect2D, GameSettingsData.VfxVolume);
            ShopPanel.SetActive(false);
        }

        private void ValidateItemObtained(Selectable button, bool isItemObtained) {
            if (isItemObtained) {
                button.interactable = false;
            }
        }
        
    }
}