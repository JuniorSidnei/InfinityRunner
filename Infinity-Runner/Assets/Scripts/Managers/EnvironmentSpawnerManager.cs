using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace InfinityRunner.Managers {
    
    public class EnvironmentSpawnerManager : MonoBehaviour {
        public List<Transform> Spawn;
        public List<GameObject> Environments;
        
        private void Start() {
            InvokeRepeating(nameof(SpawnEnvironments), 0f, 1.5f);
        }

        private void SpawnEnvironments() {
            if (!GameManager.Instance.IsGameStarted) return;
            
            var rand = Random.Range(0, Environments.Count - 1);
            Instantiate(Environments[rand].gameObject, Spawn[rand].position, Quaternion.identity, transform);
        }
    }
}