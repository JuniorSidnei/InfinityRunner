using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfinityRunner.Managers {
    
    public class CollectableSpawnerManager : MonoBehaviour {
        
        public GameObject Collectable;
        public Transform Spawn;


        private void Start() {
            InvokeRepeating(nameof(SpawnCollectable), 0.0f, 1.2f);
        }

        private void SpawnCollectable() {
            if (!GameManager.Instance.IsGameStarted) return;
            
            Instantiate(Collectable, Spawn.position, Quaternion.identity, transform);
        }

    }
}