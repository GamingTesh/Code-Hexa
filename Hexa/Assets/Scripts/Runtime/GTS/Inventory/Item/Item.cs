using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GTS.Inventory
{
    /// <summary>
    /// Base item class for items
    /// </summary>
    [System.Serializable]
    [CreateAssetMenu(fileName = "Item", menuName = "Item/baseItem")]
    public class Item : ScriptableObject
    {
        new public string name = "Default Item";
        public Sprite icon = null;

        public virtual void Use()
        {
            Debug.Log("Using " + name);
        }
    }
}
