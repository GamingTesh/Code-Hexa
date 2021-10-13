using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using GTS.Inventory;
using GTS.Manager;

namespace GTS.Inventory.UI
{
    /// <summary>
    /// Slot item for the inventory UI
    /// </summary>
    public class ItemSlot : MonoBehaviour
    {
        public Image icon;
        private Item item;

        public Item Item => item;

        public bool isBeingDragged = false;

        public virtual void AddItem(Item newItem)
        {
            item = newItem;
            icon.enabled = true;
            icon.sprite = newItem.icon;
        }

        public void UseItem()
        {
            if (item == null || isBeingDragged) return;

            if(Input.GetKey(KeyCode.LeftAlt))
            {
                GTS.Inventory.Inventory.instance.SwitchHotbarInventory(item);
            }
            else
            {
                item.Use();
            }
        }

        public void ClearSlot()
        {
            item = null;
            icon.enabled = false;
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

        public void OnCursorEnter()
        {
            if (item == null || isBeingDragged) return;

            GameManager.instance.DisplayItemInfo(item.name, item.GetItemDescription(), transform.position);
        }

        public void OnCursorExit()
        {
            if (item == null) return;

            GameManager.instance.DestroyItemInfo();
        }
    }
}
