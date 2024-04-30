using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableObject : MonoBehaviour
{
    public string ItemName;
    public bool playerInRange;

    public string GetItemName()
    {
        return ItemName;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && SelectionManager.Instance.onTarget &&
            SelectionManager.Instance.selectedObject == gameObject)
        {
            //if inv not full
            if (!InventorySystem.Instance.CheckIfFull())
            {
                    InventorySystem.Instance.AddToInventory(ItemName);//if inv not full add this item to inv
                    Destroy(gameObject);

            }
            else
            {
                Debug.Log("Inventory is full");
            }


        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}