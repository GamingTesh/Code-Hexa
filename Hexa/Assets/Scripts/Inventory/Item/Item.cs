using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using GTS.Manager;

namespace GTS.Inventory
{
    /// <summary>
    /// Base item class for items
    /// </summary>
    [System.Serializable]
    [CreateAssetMenu(fileName = "Item", menuName = "Item/baseItem")]
    public class Item : ScriptableObject
    {
        [BoxGroup("Item Info")]
        new public string name = "Default Item";

        [BoxGroup("Item Info")]
        [ShowInInspector, PreviewField(45, ObjectFieldAlignment.Left)]
        public Sprite icon = null;

        [BoxGroup("Item Info")]
        [TextArea]
        public string itemDescription;

        public virtual void Use()
        {
            Debug.Log("Using " + name);
            NotificationManager.instance.Notify("Using " + name);
        }

        public virtual string GetItemDescription()
        {
            return itemDescription;
        }
    }
}
