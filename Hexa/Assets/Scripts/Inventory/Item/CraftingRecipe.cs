using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GTS.Manager;
using GTS.Inventory.UI;

namespace GTS.Inventory
{
    /// <summary>
    /// Crafting recipes6.
    /// </summary>
    [System.Serializable]
    [CreateAssetMenu(fileName = "Item", menuName = "CraftingRecipe/baseRecipe")]
    public class CraftingRecipe : Item
    {
        public Item result;
        public Ingredient[] ingredients;
        public float craftTime = 1f;
        private CraftingSlot parentCraftingSlot;

        public CraftingSlot ParentCraftingSlot => parentCraftingSlot;

        public void SetParentSlot(CraftingSlot slot)
        {
            parentCraftingSlot = slot;
        }

        private bool CanCraft()
        {
            // ask the inventory if there are enough resources
            foreach (Ingredient ingredient in ingredients)
            {
                bool containsCurrentIngredient = Inventory.instance.ContainsItem(ingredient.item.name, ingredient.amount);

                if (!containsCurrentIngredient)
                    return false;
            }

            return true;
        }

        private void RemoveIngredientsFromInventory()
        {
            foreach (Ingredient ingredient in ingredients)
            {
                Inventory.instance.RemoveItems(ingredient.item.name, ingredient.amount);
            }
        }

        public bool CraftItem()
        {
            if (!CanCraft()) return false;

            parentCraftingSlot.DecreaseCount();
            RemoveIngredientsFromInventory();

            //start crafting
            parentCraftingSlot.StartCrafting();

            return true;
        }

        public override void Use()
        {
            parentCraftingSlot.IncreaseCount();
            Inventory.instance.AddCraftingItem(this);
        }

        public override string GetItemDescription()
        {
            string itemIngredients = "";

            foreach (Ingredient ingredient in ingredients)
            {
                itemIngredients += " - " + ingredient.amount + " " + ingredient.item.name + " \n";
            }

            return itemIngredients;
        }

        [System.Serializable]
        public class Ingredient
        {
            public Item item;
            public int amount;
        }
    }
}
