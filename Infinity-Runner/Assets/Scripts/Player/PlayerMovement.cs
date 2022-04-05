using System;
using System.Collections;
using System.Collections.Generic;
using InfinityRunner.Controllers.Raycast;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InfinityRunner.Player.Movement {
    
    public class PlayerMovement : MonoBehaviour {

        public float FallMultiplier = 2.5f;
        public float LowFallMultiplier = 2f;
        public float JumpForce;

        private Rigidbody2D m_rigidbody;
        private PlayerInput m_playerInput;
        private InputAction m_jumpAction;
        private bool m_isJumpimg;
        private RaycastController m_raycastController;

        public Vector2 Velocity => m_rigidbody.velocity;

        private void OnEnable() {
            m_playerInput = GetComponent<PlayerInput>();
            m_jumpAction = m_playerInput.actions["Jump"];
            m_jumpAction.performed += Jump;
            m_jumpAction.canceled += CancelJump;
        }

        private void OnDisable() {
            m_jumpAction.performed -= Jump;
            m_jumpAction.canceled -= CancelJump;
        }

        private void Awake() {
            m_rigidbody = GetComponent<Rigidbody2D>();
            m_raycastController = GetComponent<RaycastController>();
        }

        private void FixedUpdate() {
            if (m_rigidbody.velocity.y < 0) {
                m_rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiplier - 1) * Time.deltaTime;
            } else if (m_rigidbody.velocity.y > 0 && !m_isJumpimg) {
                m_rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (LowFallMultiplier - 1) * Time.deltaTime;
            }
        }

        private void Jump(InputAction.CallbackContext callbackContext) {
            m_isJumpimg = true;
            if (m_raycastController.IsGrounded) {
                m_rigidbody.velocity = Vector2.up * JumpForce;
            }
        }
        
        private void CancelJump(InputAction.CallbackContext callbackContext) {
            m_isJumpimg = false;
        }
    }
}