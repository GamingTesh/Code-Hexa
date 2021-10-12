using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using GTS.Inventory;
using GTS.Manager;

namespace GTS.UI.Inventory
{
    /// <summary>
    /// Handles interfacing the inventory with the character allowing interaction
    /// </summary>
    public class InventoryUI : MonoBehaviour
    {
        [BoxGroup("Elements")]
        public GameObject inventoryParent;
        [BoxGroup("Elements")]
        public GameObject inventoryTab;
        [BoxGroup("Elements")]
        public GameObject craftingTab;

        [BoxGroup("References")]
        public GameObject inventorySlotPrefab;
        [BoxGroup("References")]
        public GameObject craftingSlotPrefab;

        [BoxGroup("References UI")]
        public Transform inventoryItemTransform;
        [BoxGroup("References UI")]
        public Transform craftingItemTransform;

        // Stores list of current slots
        private List<ItemSlot> itemSlotList = new List<ItemSlot>();

        // private boolean that holds current status on inventoryOpenUI
        private bool inventoryOpen = false;

        // Gets the current status for inventoryOpen
        public bool InventoryOpen => inventoryOpen;

        //Unity Methods
        private void Awake()
        {
            CloseInventory();
        }

        private void Start()
        {
            GTS.Inventory.Inventory.instance.onItemChange += UpdateInventoryUI;
            UpdateInventoryUI();
            SetupCraftingRecipes();
        }

        // Crafting Method
        private void SetupCraftingRecipes()
        {
            List<Item> craftingRecipes = GameManager.instance.craftingRecipesList;

            foreach (Item recipe in craftingRecipes)
            {
                GameObject GO = Instantiate(craftingSlotPrefab, craftingItemTransform);
                ItemSlot slot = GO.GetComponent<ItemSlot>();
                slot.AddItem(recipe);
            }
        }

        // Utility Methods
        private void UpdateInventoryUI()
        {
            int currentItemCount = GTS.Inventory.Inventory.instance.inventoryItemList.Count;

            if(currentItemCount > itemSlotList.Count)
            {
                // Add more item slots
                AddItemSlots(currentItemCount);
            }

            for (int i = 0; i < itemSlotList.Count; i++)
            {
                if(i < currentItemCount)
                {
                    // Update the current item in the slot
                    itemSlotList[i].AddItem(GTS.Inventory.Inventory.instance.inventoryItemList[i]);
                }
                else
                {
                    itemSlotList[i].DestroySlot();
                    itemSlotList.RemoveAt(i);
                }
            }
        }

        private void AddItemSlots(int currentItemCount)
        {
            int amount = currentItemCount - itemSlotList.Count;

            for (int i = 0; i < amount; i++)
            {
                GameObject GO = Instantiate(inventorySlotPrefab, inventoryItemTransform);
                ItemSlot newSlot = GO.GetComponent<ItemSlot>();
                itemSlotList.Add(newSlot);
            }
        }

        private void OpenInventory()
        {
            inventoryOpen = true;
            inventoryParent.SetActive(true);
        }

        private void CloseInventory()
        {
            inventoryOpen = false;
            inventoryParent.SetActive(false);
        }

        // Input Handling Methods
        private void OnInventory()
        {
            if (inventoryOpen)
                CloseInventory();
            else
                OpenInventory();
        }

        // UI Event Methods
        public void OnInventoryTabClicked()
        {
            craftingTab.SetActive(false);
            inventoryTab.SetActive(true);
        }

        public void OnCraftingTabClicked()
        {
            craftingTab.SetActive(true);
            inventoryTab.SetActive(false);
        }
    }
}
