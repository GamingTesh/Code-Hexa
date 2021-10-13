using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

namespace GTS.Inventory.UI
{
    /// <summary>
    /// Handles displaying info for items in UI
    /// </summary>
    public class ItemInfo : MonoBehaviour
    {
        [BoxGroup("References")]
        public TextMeshProUGUI itemName;
        [BoxGroup("References")]
        public TextMeshProUGUI itemDescription;

        public void Setup(string name, string description)
        {
            itemName.text = name;
            itemDescription.text = description;
        }
    }
}
