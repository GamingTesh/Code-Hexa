using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using GTS.Inventory;
using GTS.Inventory.UI;

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

        [TabGroup("Lists")]
        public List<Item> itemList = new List<Item>();
        [TabGroup("Lists")]
        public List<Item> craftingRecipesList = new List<Item>();

        [TabGroup("UI")]
        public Transform canvas;
        [TabGroup("UI")]
        public Transform hotbarTransform;
        [TabGroup("UI")]
        public Transform inventoryTransform;

        [TabGroup("Item Info UI")]
        public GameObject itemInfoPrefab;
        [TabGroup("Item Info UI")]
        public float moveX = 0;
        [TabGroup("Item Info UI")]
        public float moveY = 0;

        private GameObject currentItemInfo = null;

        public void AddItem()
        {
            Item newItem = itemList[Random.Range(0, itemList.Count)];
            Inventory.Inventory.instance.AddItem(Instantiate(newItem));
        }

        public void OnStatItemUse(StatItemType itemType, int amount)
        {
            Debug.Log("Consuming " + itemType + " Add amount: " + amount);
        }

        public void DisplayItemInfo(string itemName, string itemDescription, Vector2 buttonPos)
        {
            if(currentItemInfo != null)
            {
                Destroy(currentItemInfo.gameObject);
            }

            buttonPos.x += moveX;
            buttonPos.y += moveY;

            currentItemInfo = Instantiate(itemInfoPrefab, buttonPos, Quaternion.identity, canvas);
            currentItemInfo.GetComponent<ItemInfo>().Setup(itemName, itemDescription);
        }

        public void DestroyItemInfo()
        {
            if(currentItemInfo != null)
            {
                Destroy(currentItemInfo.gameObject);
            }
        }
    }
}
