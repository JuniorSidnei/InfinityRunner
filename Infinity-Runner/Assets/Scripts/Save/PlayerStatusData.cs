using System;
using System.Collections;
using System.Collections.Generic;
using InfinityRunner.Scriptables;
using InfinityRunner.Utils.Collectables;
using UnityEngine;

namespace InfinityRunner.Save {

    [Serializable]
    public class PlayerStatusData {
        
        public int Coins;
        public int Life;
        public float Speed;
        public int CoinMultiplier;
        public bool Shield;
        public bool Shoot;
        public bool Collector;
        public bool Respawn;

        public PlayerStatusData(PlayerStatus playerStatus) {
            Coins = playerStatus.Coins;
            Life = playerStatus.Life;
            Speed = playerStatus.Speed;
            CoinMultiplier = playerStatus.CoinMultiplier;
            Shield = playerStatus.Shield;
            Shoot = playerStatus.Shoot;
            Collector = playerStatus.Collector;
            Respawn = playerStatus.Respawn;
        }
    }
}