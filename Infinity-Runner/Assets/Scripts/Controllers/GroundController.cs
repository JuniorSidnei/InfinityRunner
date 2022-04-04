using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using InfinityRunner.Managers.Ground;
using UnityEngine;

namespace InfinityRunner.Controllers {

    public class GroundController : MonoBehaviour {
        public GameObject Ground;

        private List<GameObject> m_grounds = new List<GameObject>();
        private Vector3 NextSpawnPosition;
        private int m_indexList = 0;
        private GameObject m_lastGround;

        private void OnEnable()
        {
            MoveObstacle.onGroundReallocated += OnGroundReallocated;
        }
        
        private void OnDisable()
        {
            MoveObstacle.onGroundReallocated -= OnGroundReallocated;
        }

        private void Start() {
            NextSpawnPosition = transform.position;
            for (var i = 0; i < 5; i++) {
                SpawnGround();
            }
        }

        private void FixedUpdate() {
            NextSpawnPosition = m_lastGround.transform.GetChild(0).transform.position;
        }

        private void SpawnGround() {
            var tempGround = Instantiate(Ground, NextSpawnPosition, Quaternion.identity, transform);
            NextSpawnPosition = tempGround.transform.GetChild(0).transform.position;
            m_lastGround = tempGround;
            //m_grounds.Add(tempGround);
        }

        private void OnGroundReallocated(GameObject ground) {
            ground.transform.position = NextSpawnPosition - new Vector3(0.2f, 0f, 0);
            m_lastGround = ground;
        }
    }
    
}
