using System.Collections;
using System.Collections.Generic;
using InfinityRunner.Managers;
using UnityEngine;

namespace InfinityRunner.Player {
    
    public class PlayerLife : MonoBehaviour {
        
        public int Life = 5;
        public LayerMask ObstacleLayer;
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (((1 << other.gameObject.layer) & ObstacleLayer) == 0) {
                return;
            }

            Life -= 1;
            HudManager.Instance.UpdateLife(Life);
        }
    }
}