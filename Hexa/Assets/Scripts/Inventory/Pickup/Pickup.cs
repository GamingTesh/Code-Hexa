using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GTS.Inventory.Pickup
{
    public class Pickup : MonoBehaviour
    {
        public Item item;

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Player")
            {
                Item newItem = item;
                other.GetComponent<Inventory>().AddItem(Instantiate(newItem));
                Destroy(gameObject);
            }
        }
    }
}

