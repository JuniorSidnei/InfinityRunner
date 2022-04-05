using System;
using System.Collections.Generic;
using InfinityRunner.Utils;
using Unity.Mathematics;
using UnityEngine;

namespace InfinityRunner.Managers {

    public class HudManager : Singleton<HudManager> {
        public GameObject StartGameText;
        
        [Header("life settings")]
        public GameObject LifeObject;
        public Transform LifeContainer;

        [Header("end game settings")]
        public GameObject EndGamePanel;
        
        private List<GameObject> m_lifes = new List<GameObject>();

        private void OnEnable() {
            GameManager.onGameStarted += GameStarted;
        }

        private void OnDisable() {
            GameManager.onGameStarted -= GameStarted;
        }

        private void Start() {
            //this number 5 will be the amount of life that player have in the scriptable
            for (var i = 0; i < 5; i++) {
                var life = Instantiate(LifeObject, LifeContainer);
                life.transform.localPosition = new Vector3(i * 80, 0, 0);
                m_lifes.Add(life);
            }
            
        }

        private void GameStarted() {
            StartGameText.SetActive(false);
        }

        public void UpdateLife(int index) {
            var star = m_lifes[index].transform.GetChild(0).GetComponent<AnimateStarLife>();
            star.AnimateStar();

            if (index <= 0) {
                EndGamePanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}