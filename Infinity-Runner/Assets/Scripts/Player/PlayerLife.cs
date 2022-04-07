using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using InfinityRunner.Managers;
using InfinityRunner.Save;
using InfinityRunner.Scriptables;
using InfinityRunner.Utils;
using UnityEngine;

namespace InfinityRunner.Player {
    
    public class PlayerLife : MonoBehaviour {

        public PlayerStatus PlayerStatus;
        public LayerMask ObstacleLayer;
        public AudioClip HurtSound;
        public Animator Animator;
        private int m_life;
        
        private void Awake() {
            m_life = PlayerStatus.Life;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (((1 << other.gameObject.layer) & ObstacleLayer) == 0) {
                return;
            }

            m_life -= 1;
            if (m_life <= 0) {
                Animator.SetTrigger("hurt");
                Camera.main.DOShakePosition(0.2f, 0.2f);
                AudioController.Instance.Play(HurtSound, AudioController.SoundType.SoundEffect2D, GameManager.Instance.GetVfxVolume());
                SaveSystem.SavePlayerStatus(PlayerStatus);
            }
            
            HudManager.Instance.UpdateLife(m_life);
        }
    }
}