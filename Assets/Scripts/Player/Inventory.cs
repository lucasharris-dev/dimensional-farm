using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    int playerMoney = 0;

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

    void SelectItem()
    {
        if (keyboard == null)
        {
            return;
        }

        if (keyboard.digit1Key.isPressed)
        {
            if (equippedItem == GameObject.FindGameObjectWithTag("FarmingTool"))
            {
                return;
            }

            equippedItem.GetComponent<SpriteRenderer>().enabled = false;
            equippedItem = GameObject.FindGameObjectWithTag("FarmingTool");
        }
        if (keyboard.digit2Key.isPressed)
        {
            if (equippedItem == GameObject.FindGameObjectWithTag("SeedPouch"))
            {
                return;
            }

            equippedItem.GetComponent<SpriteRenderer>().enabled = false;
            equippedItem = GameObject.FindGameObjectWithTag("SeedPouch");
        }
        if (keyboard.digit3Key.isPressed)
        {
            if (equippedItem == GameObject.FindGameObjectWithTag("WaterCan"))
            {
                return;
            }

            equippedItem.GetComponent<SpriteRenderer>().enabled = false;
            equippedItem = GameObject.FindGameObjectWithTag("WaterCan");
        }
        if (keyboard.digit4Key.isPressed)
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

    public GameObject GetEquippedItem()
    {
        return equippedItem;
    }

    public void SetPlayerMoney(int amount)
    {
        playerMoney += amount;
        Debug.Log(playerMoney);
    }
}
