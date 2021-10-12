using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private bool CanCraft()
        {
            // ask the inventory if there are enough resources
            foreach (Ingredient ingredient in ingredients)
            {
                bool containsCurrentIngredient = Inventory.instance.ContainsItem(ingredient.item, ingredient.amount);

                if (!containsCurrentIngredient)
                    return false;
            }

            return true;
        }

        private void RemoveIngredientsFromInventory()
        {
            foreach (Ingredient ingredient in ingredients)
            {
                Inventory.instance.RemoveItems(ingredient.item, ingredient.amount);
            }
        }

        public override void Use()
        {
            if(CanCraft())
            {
                // remove items
                RemoveIngredientsFromInventory();

                // add item to inventory
                Inventory.instance.AddItem(result);
                Debug.Log("You just crafted a: " + result.name);
            }
            else
            {
                Debug.Log("You dont have enough ingredients to craft: " + result.name);
            }
        }

        [System.Serializable]
        public class Ingredient
        {
            public Item item;
            public int amount;
        }
    }
}
