using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using GTS.Manager;

namespace GTS.Inventory
{
    /// <summary>
    /// Inventory component
    /// </summary>
    public class Inventory : MonoBehaviour
    {
        #region singleton
        public static Inventory instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        #endregion

        public delegate void OnItemChange();
        public OnItemChange onItemChange = delegate {};

        public List<Item> inventoryItemList = new List<Item>();

        public List<Item> hotBarItemList = new List<Item>();

        public HotbarController hotbarController;

        private Queue<CraftingRecipe> craftingQueue = new Queue<CraftingRecipe>();
        private bool isCrafting = false;

        public void SwitchHotbarInventory(Item item)
        {
            // inventory to hotbar
            foreach (Item i in inventoryItemList)
            {
                if(i == item)
                {
                    if(hotBarItemList.Count >= hotbarController.hotbarSlotSize)
                    {
                        NotificationManager.instance.Notify("No more slots");
                    }
                    else
                    {
                        hotBarItemList.Add(item);
                        inventoryItemList.Remove(item);
                        onItemChange.Invoke();
                    }

                    return;
                }
            }

            // hotbar to inventory
            foreach (Item i in hotBarItemList)
            {
                if(i == item)
                {
                    hotBarItemList.Remove(item);
                    inventoryItemList.Add(item);
                    onItemChange.Invoke();
                    return;
                }
            }

        }

        public void AddItem(Item item)
        {
            inventoryItemList.Add(item);
            onItemChange.Invoke();
        }

        public void RemoveItem(Item item)
        {
            if(inventoryItemList.Contains(item))
                inventoryItemList.Remove(item);
            else if(hotBarItemList.Contains(item))
                hotBarItemList.Remove(item);
            onItemChange.Invoke();
        }

        public bool ContainsItem(string itemName, int amount)
        {
            int itemCounter = 0;

            foreach (Item i in inventoryItemList)
            {
                if(i.name == itemName)
                {
                    itemCounter++;
                }
            }

            foreach (Item i in hotBarItemList)
            {
                if (i.name == itemName)
                {
                    itemCounter++;
                }
            }

            if (itemCounter >= amount)
                return true;
            else
                return false;
        }

        public void RemoveItems(string itemName, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                RemoveItemType(itemName);
            }
        }

        public void RemoveItemType(string itemName)
        {
            foreach (Item i in inventoryItemList)
            {
                if (i.name == itemName)
                {
                    inventoryItemList.Remove(i);
                    return;
                }
            }

            foreach (Item i in hotBarItemList)
            {
                if (i.name == itemName)
                {
                    hotBarItemList.Remove(i);
                    return;
                }
            }

        }

        public void AddCraftingItem(CraftingRecipe newRecipe)
        {
            craftingQueue.Enqueue(newRecipe);

            if(!isCrafting)
            {
                isCrafting = true;
                //start crafting
                StartCoroutine(CraftItem());
            }
        }

        private IEnumerator CraftItem()
        {
            //check if queue is emepty
            if(craftingQueue.Count == 0)
            {
                isCrafting = false;
                yield break;
            }

            CraftingRecipe currentRecipe = craftingQueue.Dequeue();

            //check if we have enoughg resources
            if(!currentRecipe.CraftItem())
            {
                ResetCraftingText();
                craftingQueue.Clear();
                isCrafting = false;
                yield break;
            }

            yield return new WaitForSeconds(currentRecipe.craftTime * 1.1f);

            //add item inventory
            AddItem(currentRecipe.result);

            // check if continue crafting
            if(craftingQueue.Count > 0)
            {
                yield return StartCoroutine(CraftItem());
            }
            else
            {
                isCrafting = false;
            }
        }

        private void ResetCraftingText()
        {
            foreach (CraftingRecipe recipe in craftingQueue)
            {
                recipe.ParentCraftingSlot.ResetCount();
            }
        }

    }
}
