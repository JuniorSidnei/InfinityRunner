using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfinityRunner.Scriptables {
    
    [CreateAssetMenu(menuName = "InfinityRunner/PlayerStatus")]
    public class PlayerStatus : ScriptableObject {
        public int Life;
        public int Coins;
        public float Speed;
        public int CoinMultiplier;
        public int Shield;
        public int Collector;
        public int Shoot;
        public int Respawn;
    }
}