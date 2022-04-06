using UnityEngine;

namespace InfinityRunner.Scriptables {
    
    [CreateAssetMenu(menuName = "InfinityRunner/GameSettingsData")]
    public class GameSettingsData : ScriptableObject {
        public float VfxVolume;
        public float MusicVolume;
        public bool IsFullscreen;
        public int ResolutionIndex;
    }
}