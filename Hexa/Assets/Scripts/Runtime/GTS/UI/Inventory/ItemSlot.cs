using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using GTS.Inventory;

namespace GTS.UI.Inventory
{
    /// <summary>
    /// Slot item for the inventory UI
    /// </summary>
    public class ItemSlot : MonoBehaviour
    {
        public Image icon;
        private Item item;

        public void AddItem(Item newItem)
        {
            item = newItem;
            icon.sprite = newItem.icon;
        }

        public void UseItem()
        {
            if (item != null)
                item.Use();
        }

        public void DestroySlot()
        {
            Destroy(gameObject);
        }

        public void OnRemoveButtonClicked()
        {
            if(item != null)
            {
                GTS.Inventory.Inventory.instance.RemoveItem(item);
            }
        }
    }
}
