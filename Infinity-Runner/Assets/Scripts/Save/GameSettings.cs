using System;
using InfinityRunner.Scriptables;

namespace InfinityRunner.Managers {
    
    [Serializable]
    public class GameSettings {
        public float VfxVolume;
        public float MusicVolume;
        public bool IsFullscreen;
        public int ResolutionIndex;

        public GameSettings(GameSettingsData settings) {
            
            VfxVolume = settings.VfxVolume;
            MusicVolume = settings.MusicVolume;
            IsFullscreen = settings.IsFullscreen;
            ResolutionIndex = settings.ResolutionIndex;
        }
    }
}