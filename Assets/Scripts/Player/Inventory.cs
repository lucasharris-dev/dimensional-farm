using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    int playerMoney = 0;
    int lifetimeProfit = 0;
    string seedTag = "Seed";
    string grownCropsTag = "GrownCrops";
    string mineralTag = "Mineral";
    List<GameObject> playerSellableInventory = new List<GameObject>();
    List<GameObject> playerSeedInventory = new List<GameObject>();

    Keyboard keyboard;
    GameObject equippedItem;

    void Awake()
    {
        keyboard = Keyboard.current;
        equippedItem = GameObject.FindGameObjectWithTag("FarmingTool");
        equippedItem.GetComponent<SpriteRenderer>().enabled = true;
    }

    void Update()
    {
        SelectItem();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PickUpItem(other.gameObject);
    }

    void SelectItem()
    {
        if (keyboard == null)
        {
            return;
        }

        if (keyboard.upArrowKey.isPressed)
        {
            if (equippedItem == GameObject.FindGameObjectWithTag("FarmingTool"))
            {
                return;
            }

            equippedItem.GetComponent<SpriteRenderer>().enabled = false;
            equippedItem = GameObject.FindGameObjectWithTag("FarmingTool");
        }
        if (keyboard.downArrowKey.isPressed)
        {
            if (equippedItem == GameObject.FindGameObjectWithTag("SeedPouch"))
            {
                return;
            }

            equippedItem.GetComponent<SpriteRenderer>().enabled = false;
            equippedItem = GameObject.FindGameObjectWithTag("SeedPouch");
        }
        if (keyboard.rightArrowKey.isPressed)
        {
            if (equippedItem == GameObject.FindGameObjectWithTag("WaterCan"))
            {
                return;
            }

            equippedItem.GetComponent<SpriteRenderer>().enabled = false;
            equippedItem = GameObject.FindGameObjectWithTag("WaterCan");
        }
        if (keyboard.leftArrowKey.isPressed)
        {
            if (equippedItem == GameObject.FindGameObjectWithTag("CropBag"))
            {
                return;
            }

            equippedItem.GetComponent<SpriteRenderer>().enabled = false;
            equippedItem = GameObject.FindGameObjectWithTag("CropBag");
        }
        equippedItem.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void PickUpItem(GameObject item)
    {
        if (item.tag == seedTag)
        {
            playerSeedInventory.Add(item);
        }
        else if (item.tag == grownCropsTag || item.tag == mineralTag)
        {
            playerSellableInventory.Add(item);
        }
        for (int i = 0; i < playerSeedInventory.Count; i++)
        {
            Debug.Log("seed inventory: " + playerSeedInventory[i]);
        }
        for (int i = 0; i < playerSellableInventory.Count; i++)
        {
            Debug.Log("inventory: " + playerSellableInventory[i]);
        }
    }

    public GameObject GetEquippedItem()
    {
        return equippedItem;
    }

    public List<GameObject> GetPlayerSellableInventory()
    {
        return playerSellableInventory;
    }

    public List<GameObject> GetPlayerSeedInventory()
    {
        return playerSeedInventory;
    }

    public void SetPlayerMoney(int amount)
    {
        playerMoney += amount;
        lifetimeProfit += playerMoney;
    }
}
