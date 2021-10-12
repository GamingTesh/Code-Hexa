using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GTS.Inventory
{
    /// <summary>
    /// Hold the stats for items
    /// </summary>  
    [System.Serializable]
    [CreateAssetMenu(fileName = "StatItem", menuName = "Item/StatItem")]
    public class StatItem : Item
    {
        public StatItemType itemType;
        public int amount;

        public override void Use()
        {
            base.Use();
            GTS.Manager.GameManager.instance.OnStatItemUse(itemType, amount);
            Inventory.instance.RemoveItem(this);
        }
    }

    public enum StatItemType
    {
        HealthItem,
        ThirstItem,
        FoodItem
    }
}