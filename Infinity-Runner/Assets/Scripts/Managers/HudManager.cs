using InfinityRunner.Utils;
using UnityEngine;

namespace InfinityRunner.Managers {

    public class HudManager : Singleton<HudManager> {
        public GameObject StartGameText;

        private void OnEnable() {
            GameManager.onGameStarted += GameStarted;
        }

        private void OnDisable() {
            GameManager.onGameStarted -= GameStarted;
        }

        private void GameStarted() {
            StartGameText.SetActive(false);
        }
    }
}