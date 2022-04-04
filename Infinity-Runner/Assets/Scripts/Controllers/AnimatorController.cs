using System;
using System.Collections;
using System.Collections.Generic;
using InfinityRunner.Controllers.Raycast;
using InfinityRunner.Managers;
using InfinityRunner.Player.Movement;
using UnityEngine;

namespace InfinityRunner.Controllers.PlayerAnimator {
    
    public class AnimatorController : MonoBehaviour {

        public RaycastController RaycastController;
        public PlayerMovement PlayerMovement;
        private Animator m_animator;

        private void OnEnable() {
            GameManager.onGameStarted += GameStarted;
        }

        private void OnDisable() {
            GameManager.onGameStarted -= GameStarted;
        }

        private void GameStarted() {
            m_animator.SetTrigger("running");
        }
        
        private void Awake() {
            m_animator = GetComponent<Animator>();
        }

        private void Update() {
            m_animator.SetBool("grounded", RaycastController.IsGrounded);
            m_animator.SetFloat("velY", PlayerMovement.Velocity.y);
        }
    }
}