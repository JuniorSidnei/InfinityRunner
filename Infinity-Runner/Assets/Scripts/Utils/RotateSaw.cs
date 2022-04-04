using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfinityRunner.Utils {
    
    public class RotateSaw : MonoBehaviour {
        public float RotationSpeed;

        private void FixedUpdate() {
            transform.Rotate(new Vector3(0, 0, RotationSpeed * Time.deltaTime), Space.Self);
        }
    }
}