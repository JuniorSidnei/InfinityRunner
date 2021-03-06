using System;
using System.Collections.Generic;
using InfinityRunner.Scriptables;
using InfinityRunner.Utils;
using TMPro;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace InfinityRunner.Managers {

    public class HudManager : Singleton<HudManager> {
        public GameObject StartGameText;
        
        [Header("life settings")]
        public GameObject LifeObject;
        public Transform LifeContainer;

        [Header("end game settings")]
        public GameObject EndGamePanel;
        public TextMeshProUGUI EndGameScoreText;
        public Button RetryBtn;
        public Button HubBtn;
        
        [Header("score settings")]
        public TextMeshProUGUI ScoreText;

        [Header("shoot settings")]
        public TextMeshProUGUI BulletAmount;
        public GameObject BulletContainer;
        
        public PlayerStatus PlayerStatus;
        public GameSettingsData GameSettingsData;
        public AudioClip ClickButton;
        
        private List<GameObject> m_lifes = new List<GameObject>();

        private void OnEnable() {
            GameManager.onGameStarted += GameStarted;
        }

        private void OnDisable() {
            GameManager.onGameStarted -= GameStarted;
        }

        private void Start() {
            for (var i = 0; i < PlayerStatus.Life; i++) {
                var life = Instantiate(LifeObject, LifeContainer);
                life.transform.localPosition = new Vector3(i * 50, 0, 0);
                m_lifes.Add(life);
            }
            
            RetryBtn.onClick.AddListener(() => {
                Time.timeScale = 1;
                AudioController.Instance.Play(ClickButton, AudioController.SoundType.SoundEffect2D, GameSettingsData.VfxVolume);
                SceneManager.LoadScene("GameScene");
            });
            
            HubBtn.onClick.AddListener(ShowHubScene);
            
            UpdateScore(PlayerStatus.Coins);
        
            if (!PlayerStatus.Shoot) return;
            
            BulletContainer.SetActive(true);
        }

        private void GameStarted() {
            StartGameText.SetActive(false);
        }

        public void UpdateLife(int index) {
            var star = m_lifes[index].transform.GetChild(0).GetComponent<AnimateStarLife>();
            star.AnimateStar();

            if (index <= 0) {
                Invoke(nameof(EnableEndGamePanel), 0.5f);
            }
        }

        public void UpdateScore(int currentScore) {
            ScoreText.text = currentScore.ToString();
            EndGameScoreText.text = currentScore.ToString();
            PlayerStatus.Coins = currentScore;
        }

        public void UpdateBulletAmount(int amount) {
            BulletAmount.text = amount.ToString();
        }
        
        private void EnableEndGamePanel() {
            AudioController.Instance.Stop(AudioController.SoundType.Music);
            EndGamePanel.SetActive(enabled);
            Time.timeScale = enabled ? 0 : 1;
        }

        private void ShowHubScene() {
            Time.timeScale = 1;
            AudioController.Instance.Play(ClickButton, AudioController.SoundType.SoundEffect2D, GameSettingsData.VfxVolume);
            SceneManager.LoadScene("Hub");
        }
    }
}