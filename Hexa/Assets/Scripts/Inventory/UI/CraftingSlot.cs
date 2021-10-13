using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GTS.Inventory.UI
{
    /// <summary>
    /// Slot item for crafting
    /// </summary>
    public class CraftingSlot : ItemSlot
    {
        public GameObject craftingImageGO;
        private Image craftingImage;
        private float craftTime;

        public GameObject craftingTextGO;
        private TextMeshProUGUI craftingText;
        private int currentCount = 0;

        public override void AddItem(Item newItem)
        {
            base.AddItem(newItem);
            craftingImage = craftingImageGO.GetComponent<Image>();
            craftingImage.sprite = newItem.icon;
            craftingImageGO.SetActive(false);
            craftTime = ((CraftingRecipe)newItem).craftTime;

            craftingText = craftingTextGO.GetComponent<TextMeshProUGUI>();
            craftingTextGO.SetActive(false);
        }

        public void IncreaseCount()
        {
            currentCount++;
            if(gameObject.activeInHierarchy)
            {
                craftingText.text = currentCount.ToString();
                craftingTextGO.SetActive(true);
            }
        }

        public void DecreaseCount()
        {
            currentCount--;
            if(currentCount == 0)
            {
                craftingTextGO.SetActive(false);
            }
            else if(gameObject.activeInHierarchy)
            {
                craftingText.text = currentCount.ToString();
                craftingTextGO.SetActive(true);
            }
        }

        public void ResetCount()
        {
            currentCount = 0;
            craftingTextGO.SetActive(false);
        }

        private void OnEnable()
        {
            if(currentCount > 0)
            {
                craftingText.text = currentCount.ToString();
                craftingTextGO.SetActive(true);
            }
            else
            {
                craftingTextGO.SetActive(false);
            }
        }

        public void StartCrafting()
        {
            if(gameObject.activeInHierarchy == true)
                StartCoroutine(CraftingAnimation());
        }

        private IEnumerator CraftingAnimation()
        {
            float timeElapsed = 0f;
            craftingImageGO.SetActive(true);
            craftingImage.fillAmount = 1f;

            while(timeElapsed < craftTime)
            {
                timeElapsed += Time.deltaTime;
                craftingImage.fillAmount = Mathf.Lerp(1f, 0f, timeElapsed / craftTime);
                yield return null;
            }
            craftingImageGO.SetActive(false);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            craftingImageGO.SetActive(false);
        }
    }
}
