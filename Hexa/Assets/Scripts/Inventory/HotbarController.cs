using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GTS.Inventory.UI;

namespace GTS.Inventory
{
    /// <summary>
    /// Controls the hotbar
    /// </summary>
    public class HotbarController : MonoBehaviour
    {
        public int hotbarSlotSize => gameObject.transform.childCount;
        private List<ItemSlot> hotbarSlots = new List<ItemSlot>();

        KeyCode[] hotbarKeys = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6 };

        private void Start()
        {
            SetupHotbarSlots();
            GTS.Inventory.Inventory.instance.onItemChange += UpdateHotbarUI;
        }

        private void Update()
        {
            for (int i = 0; i < hotbarKeys.Length; i++)
            {
                if(Input.GetKeyDown(hotbarKeys[i]))
                {
                    //use item
                    hotbarSlots[i].UseItem();
                    return;
                }
            }
        }

        private void UpdateHotbarUI()
        {
            int currentUseSlotCount = GTS.Inventory.Inventory.instance.hotBarItemList.Count;
            for (int i = 0; i < hotbarSlotSize; i++)
            {
                if(i < currentUseSlotCount)
                {
                    hotbarSlots[i].AddItem(Inventory.instance.hotBarItemList[i]);
                }
                else
                {
                    hotbarSlots[i].ClearSlot();
                }
            }
        }

        private void SetupHotbarSlots()
        {
            for (int i = 0; i < hotbarSlotSize; i++)
            {
                ItemSlot slot = gameObject.transform.GetChild(i).GetComponent<ItemSlot>();
                hotbarSlots.Add(slot);
            }   
        }

    }
}
