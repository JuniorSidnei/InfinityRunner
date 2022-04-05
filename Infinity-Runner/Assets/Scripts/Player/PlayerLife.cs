using System;
using System.Collections;
using System.Collections.Generic;
using InfinityRunner.Managers;
using InfinityRunner.Scriptables;
using UnityEngine;

namespace InfinityRunner.Player {
    
    public class PlayerLife : MonoBehaviour {

        public PlayerStatus PlayerStatus;
        public LayerMask ObstacleLayer;

        private int m_life;
        
        private void Awake() {
            m_life = PlayerStatus.Life;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (((1 << other.gameObject.layer) & ObstacleLayer) == 0) {
                return;
            }

            m_life -= 1;
            HudManager.Instance.UpdateLife(m_life);
        }
    }
}