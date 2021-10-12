using UnityEngine;
using GTS.UI.Inventory;
using GTS.Inventory;
using System.Collections.Generic;

namespace GTS.Player
{
    /// <summary>
    /// Handles connections between player and components
    /// </summary>
    public class Player : MonoBehaviour
    {
        // Reference to InventoryUI component
        private InventoryUI inventoryUI;

        // Hold behaviours that need to be disabled on inventory open
        public Behaviour[] behavioursToDisable;

        // Unity Methods
        private void Start()
        {
            inventoryUI = GetComponent<InventoryUI>();
        }

        private void Update()
        {
            if (inventoryUI.InventoryOpen)
            {
                ChangeCursorState(false);
                SetBehavioursEnabled(false);
            }
            else
            {
                ChangeCursorState(true);
                SetBehavioursEnabled(true);
            }
        }

        // Utility Methods
        private void SetBehavioursEnabled(bool value)
        {
            foreach (Behaviour script in behavioursToDisable)
            {
                script.enabled = value;
            }
        }

        private void ChangeCursorState(bool lockCursor)
        {
            if (lockCursor)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
        }

        // Input Handles
        // Test [WILL BE REMOVED]
        private void OnAddItem()
        {
            GTS.Manager.GameManager.instance.AddItem();
        }
    }
}
