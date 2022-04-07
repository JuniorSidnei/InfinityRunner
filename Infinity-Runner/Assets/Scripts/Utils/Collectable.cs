using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfinityRunner.Utils.Collectables {
    
    public class Collectable : MonoBehaviour {
        
        public LayerMask PlayerLayer;
        public GameObject Sparkle;
        
        public delegate void OnCollectablePicked();
        public static event OnCollectablePicked onCollectablePicked;
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (((1 << other.gameObject.layer) & PlayerLayer) == 0) {
                return;
            }

            Instantiate(Sparkle, transform.position, Quaternion.identity);
            onCollectablePicked?.Invoke();
            Destroy(gameObject);
        }
    }
}