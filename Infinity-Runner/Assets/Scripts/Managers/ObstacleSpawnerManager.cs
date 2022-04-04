using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace InfinityRunner.Managers {
    
    public class ObstacleSpawnerManager : MonoBehaviour
    {

        public List<GameObject> Obstacles;
        public Transform ObstacleSpawn;

        private void Start() {
            InvokeRepeating(nameof(SpawnObstacle), 1, 2f);
        }

        private void SpawnObstacle() {
            if (!GameManager.Instance.IsGameStarted) return;
            
            var randomObstacleId = Random.Range(0, Obstacles.Count);
            Instantiate(Obstacles[randomObstacleId], ObstacleSpawn.position, Quaternion.identity, transform);
        }
    }
}