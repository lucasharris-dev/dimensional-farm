using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FarmingInventory : MonoBehaviour
{
    [SerializeField] GameObject farmingTool;
    [SerializeField] GameObject seedPouch;
    [SerializeField] GameObject waterCan;
    [SerializeField] GameObject cropBag;

    [SerializeField] GameObject beanSeed;
    [SerializeField] GameObject bean;
    [SerializeField] GameObject wheatSeed;
    [SerializeField] GameObject wheat;
    [SerializeField] GameObject cornSeed;
    [SerializeField] GameObject corn;
    [SerializeField] GameObject carrotSeed;
    [SerializeField] GameObject carrot;
    [SerializeField] GameObject manaCrystal;
    [SerializeField] GameObject manaFruit;

    string seedTag = "Seed";
    string grownCropsTag = "GrownCrops";
    string mineralTag = "Mineral";
    List<GameObject> playerSeedInventory = new List<GameObject>();

    Keyboard keyboard;
    Farming farming;
    EquippedItems equippedItemsScript;
    GameObject equippedItem;
    GameObject equippedSeed;


    void Awake()
    {
        keyboard = Keyboard.current;

        farming = GetComponentInChildren<Farming>();
        equippedItemsScript = GetComponentInChildren<EquippedItems>();
        equippedItem = farmingTool;
    }

    void Update()
    {
        SelectItem();
        SelectSeed();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PickUpItem(other.gameObject);
    }

    void SelectItem()
    {
        if (keyboard == null || farming.GetPickingSeed())
        {
            return;
        }

        if (keyboard.upArrowKey.isPressed)
        {
            if (equippedItem == farmingTool)
            {
                return;
            }

            equippedItem.GetComponent<SpriteRenderer>().enabled = false;
            equippedItem = farmingTool;
            equippedItemsScript.ShowEquippedSeed(false);

            farming.SetPickingSeed(false);
        }
        if (keyboard.downArrowKey.isPressed)
        {
            equippedItem.GetComponent<SpriteRenderer>().enabled = false;
            equippedItem = seedPouch;
            equippedItemsScript.ShowEquippedSeed(true);

            farming.SetPickingSeed(true);

            if (playerSeedInventory.Count == 0)
            {
                GiveOneSeed();
            }
        }
        if (keyboard.rightArrowKey.isPressed)
        {
            if (equippedItem == waterCan)
            {
                return;
            }

            equippedItem.GetComponent<SpriteRenderer>().enabled = false;
            equippedItem = waterCan;
            equippedItemsScript.ShowEquippedSeed(false);

            farming.SetPickingSeed(false);
        }
        if (keyboard.leftArrowKey.isPressed)
        {
            if (equippedItem == cropBag)
            {
                return;
            }

            equippedItem.GetComponent<SpriteRenderer>().enabled = false;
            equippedItem = cropBag;
            equippedItemsScript.ShowEquippedSeed(false);

            farming.SetPickingSeed(false);
        }
        //equippedItem.GetComponent<SpriteRenderer>().enabled = true;
        equippedItemsScript.UpdateEquippedItemUI();
    }

    void SelectSeed()
    {
        if (keyboard == null)
        {
            return;
        }

        // needs to bring up a menu with the player's seed inventory
        if (equippedItem == seedPouch && farming.GetPickingSeed())
        {
            if (keyboard.escapeKey.isPressed)
            {
                farming.SetPickingSeed(false);
                return;
            }

            if (keyboard.digit1Key.isPressed)
            {
                SetEquippedSeed(beanSeed);
                farming.SetPickingSeed(false);
            }
            else if (keyboard.digit2Key.isPressed)
            {
                SetEquippedSeed(wheatSeed);
                farming.SetPickingSeed(false);
            }
            else if (keyboard.digit3Key.isPressed)
            {
                SetEquippedSeed(cornSeed);
                farming.SetPickingSeed(false);
            }
            else if (keyboard.digit4Key.isPressed)
            {
                SetEquippedSeed(carrotSeed);
                farming.SetPickingSeed(false);
            }
            else if (keyboard.digit5Key.isPressed)
            {
                SetEquippedSeed(manaCrystal);
                farming.SetPickingSeed(false);
            }
        }
    }

    public void PickUpItem(GameObject item)
    {
        if (item.tag == seedTag)
        {
            if (playerSeedInventory.Count == 0) // list.Count gets length
            {
                SetEquippedSeed(item);
            }
            playerSeedInventory.Add(item);
        }
        // else if (item.tag == grownCropsTag || item.tag == mineralTag)
        // {
        //     playerSellableInventory.Add(item);
        // }
        for (int i = 0; i < playerSeedInventory.Count; i++)
        {
            Debug.Log("seed inventory: " + playerSeedInventory[i]);
        }
        // for (int i = 0; i < playerSellableInventory.Count; i++)
        // {
        //     Debug.Log("inventory: " + playerSellableInventory[i]);
        // }
    }

    public void GiveOneSeed()
    {
        playerSeedInventory.Add(beanSeed);
        SetEquippedSeed(beanSeed);
    }

    public void RemoveOneSeed(GameObject seed)
    {
        Debug.Log("remove " + seed.name);
        // if (seed amount > 0)
        // {
                // seed amount -= 1
        // }
        // get the seed, and decrease the seed amount by one
    }

    public GameObject GetEquippedItem()
    {
        return equippedItem;
    }

    public GameObject GetEquippedSeed()
    {
        return equippedSeed;
    }

    public List<GameObject> GetPlayerSeedInventory()
    {
        return playerSeedInventory;
    }

    public void SetEquippedSeed(GameObject seed)
    {
        equippedSeed = seed;
        equippedItemsScript.UpdateEquippedSeedUI();
    }
}
