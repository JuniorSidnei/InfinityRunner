using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using InfinityRunner.Scriptables;
using UnityEngine;

namespace InfinityRunner.Managers.Ground {
    
    public class MoveObstacle : MonoBehaviour {
        public float MinimumX;
        public float Speed;
        public bool IsGround;
        public bool IgnorePlayerSpeed;

        public PlayerStatus PlayerStatus;
        public delegate void OnGroundReallocated(GameObject ground);
        public static event OnGroundReallocated onGroundReallocated;
        
        private void FixedUpdate() {
            if (!GameManager.Instance.IsGameStarted) return;

            if (IgnorePlayerSpeed) {
                transform.position += new Vector3(-Speed * Time.deltaTime, 0, 0);    
            }
            else {
                transform.position += new Vector3(-PlayerStatus.Speed * Time.deltaTime, 0, 0);
            }
            

            if (transform.position.x <= MinimumX) {
                if (IsGround) {
                    onGroundReallocated?.Invoke(gameObject);
                }
                else {
                    Destroy(gameObject);    
                }
                
            }
        }
    }
}