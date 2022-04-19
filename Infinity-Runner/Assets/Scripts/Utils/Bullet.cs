using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfinityRunner.Utils {
    
    public class Bullet : MonoBehaviour {

        public float Speed;
        
        private Rigidbody2D m_rigidbody;

        private void Start() {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() {
            m_rigidbody.position += Vector2.right * Speed * Time.deltaTime;
        }
    }
}