using System;
using InfinityRunner.Save;
using InfinityRunner.Scriptables;
using InfinityRunner.Utils;
using InfinityRunner.Utils.Collectables;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace InfinityRunner.Managers {
    
    public class GameManager : Singleton<GameManager> {

        public PlayerStatus PlayerStatus;
        public GameSettingsData GameSettingsData;
        public AudioClip CoinPicked;
        public AudioClip GameStart;
        public AudioClip MusicGame;
        
        private bool m_gameStarted;
        private PlayerInput m_PlayerInput;

        public delegate void OnGameStarted();
        public static event OnGameStarted onGameStarted;

        public bool IsGameStarted => m_gameStarted;

        private int m_playerScore;

        private void OnEnable() {
            Collectable.onCollectablePicked += OnCollectablePicked;
        }

        private void OnDisable() {
            Collectable.onCollectablePicked -= OnCollectablePicked;
        }

        private void OnCollectablePicked() {
            AudioController.Instance.Play(CoinPicked, AudioController.SoundType.SoundEffect2D, GameSettingsData.VfxVolume);
            m_playerScore += (1 + PlayerStatus.CoinMultiplier);
            HudManager.Instance.UpdateScore(m_playerScore);
        }
        
        private void Awake() {
            AudioController.Instance.Stop(AudioController.SoundType.Music);
            AudioController.Instance.Play(MusicGame, AudioController.SoundType.Music, GameSettingsData.MusicVolume, true);
            SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
            m_PlayerInput = GetComponent<PlayerInput>();
            m_PlayerInput.actions["StartGame"].performed += _ => {
                if (!m_gameStarted) {
                    m_gameStarted = true;
                    AudioController.Instance.Play(GameStart, AudioController.SoundType.SoundEffect2D, GameSettingsData.VfxVolume);
                    onGameStarted?.Invoke();
                }
            };
            
            LoadPlayerStatus();
        }

        private void LoadPlayerStatus() {
            var playerData = SaveSystem.LoadPlayerStatus();
            if (playerData == null) return;

            PlayerStatus.Coins = playerData.Coins;
            PlayerStatus.Life = playerData.Life;
            PlayerStatus.Speed = playerData.Speed;
            PlayerStatus.CoinMultiplier = playerData.CoinMultiplier;
            PlayerStatus.Shield = playerData.Shield;
            PlayerStatus.Collector = playerData.Collector;
            PlayerStatus.Shoot = playerData.Shoot;

            m_playerScore = playerData.Coins;
        }

        public float GetVfxVolume() {
            return GameSettingsData.VfxVolume;
        }
    }
}