using GTS.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GTS.Manager
{
    /// <summary>
    /// Handles Game
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region singleton
        public static GameManager instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        #endregion

        public List<Item> itemList = new List<Item>();
        public List<Item> craftingRecipesList = new List<Item>();

        public void AddItem()
        {
            Inventory.Inventory.instance.AddItem(itemList[Random.Range(0, itemList.Count)]);
        }

        public void OnStatItemUse(StatItemType itemType, int amount)
        {
            Debug.Log("Consuming " + itemType + " Add amount: " + amount);
        }
    }
}
