using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject Empty;
    [SerializeField] GameObject ManaCrystal;

    List<GameObject> playerInventory = new List<GameObject>();
    List<int> inventoryItemCount = new List<int>();
    int inventorySize = 15;

    void Awake()
    {
        playerInventory.Capacity = inventorySize;
        inventoryItemCount.Capacity = inventorySize;

        // this would be where you get the inventory in the player's save
        for (int i = 0; i < playerInventory.Capacity; i++)
        {
            playerInventory.Add(Empty);
            inventoryItemCount.Add(0);
        }

        playerInventory[4] = ManaCrystal;
        inventoryItemCount[4] = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUpItem(GameObject item)
    {
        bool hasItem = false;
        bool hasFoundEmptySlot = false;
        int firstEmpty = -1;

        for (int i = 0; i < playerInventory.Capacity; i++)
        {
            // checks if the current index is the item the player picked up, and adds the item to the inventory
            if (playerInventory[i].name == item.name)
            {
                inventoryItemCount[i] += 1;
                hasItem = true;
                break;
            }
            // checks if the current index is empty and if it is the first empty slot, and saves the index if so
            else if (playerInventory[i] == Empty && !hasFoundEmptySlot)
            {
                firstEmpty = i;
                hasFoundEmptySlot = true;
            }
        }

        // if the player doesn't have the item and they have an empty slot, puts the item in the first empty slot
        if (!hasItem && firstEmpty != -1)
        {
            playerInventory[firstEmpty] = item;
            inventoryItemCount[firstEmpty] = 1;
        }

        // temp
        for (int i = 0; i < playerInventory.Capacity; i++)
        {
            Debug.Log(i + ": " + playerInventory[i] + ", " + inventoryItemCount[i]);
        }
    }
}
