using System;
using InfinityRunner.Utils;
using InfinityRunner.Utils.Collectables;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace InfinityRunner.Managers {
    
    public class GameManager : Singleton<GameManager> {
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
            m_playerScore += 1;
            HudManager.Instance.UpdateScore(m_playerScore);
        }
        
        private void Awake() {
            SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
            m_PlayerInput = GetComponent<PlayerInput>();
            m_PlayerInput.actions["StartGame"].performed += _ => {
                if (!m_gameStarted) {
                    m_gameStarted = true;
                    onGameStarted?.Invoke();
                }
            };
        }
        
    }
}