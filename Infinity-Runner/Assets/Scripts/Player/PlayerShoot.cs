using InfinityRunner.Managers;
using InfinityRunner.Scriptables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InfinityRunner.Player {
    
    public class PlayerShoot : MonoBehaviour {
        
        public Transform Spawn;
        public GameObject Bullet;
        public PlayerStatus PlayerStatus;

        private PlayerInput m_playerInput;
        private InputAction ShootAction;
        private int m_ammunitionAmount;
        
        private void Awake() {
            m_ammunitionAmount = PlayerStatus.AmmunitionAmount;
            m_playerInput = GetComponent<PlayerInput>();
            ShootAction = m_playerInput.actions["Shoot"];
            ShootAction.performed += _ => {
                if (!PlayerStatus.Shoot || m_ammunitionAmount <= 0) return;
                m_ammunitionAmount -= 1;
                HudManager.Instance.UpdateBulletAmount(m_ammunitionAmount);
                Instantiate(Bullet, Spawn.position, Quaternion.identity);
            };
        }
    }
}