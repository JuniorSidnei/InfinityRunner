using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfinityRunner.Controllers.Raycast {
    
    [RequireComponent(typeof(Rigidbody2D))]
    public class RaycastController : MonoBehaviour {

        [Header("Raycast settings")]
        public LayerMask GroundLayer;
        public float CircleRadius;
        public Transform GroundCheck;

        private bool m_isGrounded;
        
        public bool IsGrounded => m_isGrounded;
        
        private void FixedUpdate() {
            m_isGrounded = Physics2D.OverlapCircle(GroundCheck.position, CircleRadius, GroundLayer);
        }
    }
}