using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfinityRunner.Scriptables {
    
    [CreateAssetMenu(menuName = "InfinityRunner/ItemDescription")]
    public class ItemDescription : ScriptableObject
    {
        [TextArea(10, 30)]
        public string Description;
        public int ItemPrice;

    }
}