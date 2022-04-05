using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfinityRunner.Scriptables {
    
    [CreateAssetMenu(menuName = "InfinityRunner/PlayerStatus")]
    public class PlayerStatus : ScriptableObject {
        public int Life;
        public int Coins;
        public float Speed;
        public bool DoubleJump;
        public bool Shield;
        public bool CoinCollector;
        public bool Shoot;
    }
}