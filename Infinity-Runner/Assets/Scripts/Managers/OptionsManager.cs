using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfinityRunner.Managers
{


    public class OptionsManager : MonoBehaviour
    {
        public GameObject OptionsPanel;

        public void Show() {
            OptionsPanel.SetActive(true);
        }

        public void OnChangeFullscreen() {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }
}