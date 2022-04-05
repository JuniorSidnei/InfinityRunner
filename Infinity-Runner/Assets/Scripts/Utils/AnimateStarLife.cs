using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfinityRunner.Utils {
    
    public class AnimateStarLife : MonoBehaviour {
        public Animator Animator;

        public void AnimateStar() {
            Animator.SetTrigger("animate");
        }
    }
}