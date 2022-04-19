using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using InfinityRunner.Player;
using InfinityRunner.Scriptables;
using UnityEngine;

namespace InfinityRunner.Utils.Collectables {
    
    public class Collectable : MonoBehaviour {
        
        public LayerMask PlayerLayer;
        public GameObject Sparkle;
        public PlayerStatus PlayerStatus;


        private Transform m_targetTransform;
        private bool m_onCollecting;
        
        public delegate void OnCollectablePicked();
        public static event OnCollectablePicked onCollectablePicked;


        private void Update() {
            if (!m_onCollecting) return;

            transform.DOMove(m_targetTransform.position, 0.5f).SetEase(Ease.Linear);
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (((1 << other.gameObject.layer) & PlayerLayer) == 0) {
                return;
            }
        
            OnCollect();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (((1 << other.gameObject.layer) & PlayerLayer) == 0) {
                return;
            }

            if (!PlayerStatus.Collector) return;
            m_targetTransform = other.gameObject.transform;
            m_onCollecting = true;
        }

        private void OnCollect() {
            Instantiate(Sparkle, transform.position, Quaternion.identity);
            onCollectablePicked?.Invoke();
            Destroy(gameObject);
        }

    }
}