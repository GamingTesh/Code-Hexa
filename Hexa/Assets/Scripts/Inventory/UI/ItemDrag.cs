using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GTS.Manager;
using UnityEngine.UI;

namespace GTS.Inventory.UI
{
    /// <summary>
    /// use to drag items in UI
    /// </summary>
    public class ItemDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private ItemSlot itemSlot;
        private RectTransform hotbarRect;
        private RectTransform inventoryRect;

        public GameObject previewPrefab;
        private GameObject currentPreview;
        public Image icon;
        private Color baseColor;
        private bool isHotbarSlot;


        private void Start()
        {
            itemSlot = GetComponent<ItemSlot>();
            hotbarRect = GameManager.instance.hotbarTransform as RectTransform;
            inventoryRect = GameManager.instance.inventoryTransform as RectTransform;

            baseColor = icon.color;

            isHotbarSlot = RectTransformUtility.RectangleContainsScreenPoint(hotbarRect, transform.position);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            itemSlot.OnCursorExit();
            itemSlot.isBeingDragged = true;

            // change alpha
            var tmpColor = baseColor;
            tmpColor.a = 0.4f;
            icon.color = tmpColor;

            currentPreview = Instantiate(previewPrefab, GameManager.instance.canvas);
            currentPreview.GetComponent<Image>().sprite = itemSlot.Item.icon;
            currentPreview.transform.position = transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            currentPreview.transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            itemSlot.isBeingDragged = false;
            icon.color = baseColor;

            if((RectTransformUtility.RectangleContainsScreenPoint(hotbarRect, Input.mousePosition) && !isHotbarSlot) || (RectTransformUtility.RectangleContainsScreenPoint(inventoryRect, Input.mousePosition) && isHotbarSlot))
            {
                GTS.Inventory.Inventory.instance.SwitchHotbarInventory(itemSlot.Item);
            }

            Destroy(currentPreview);
        }
    }
}
