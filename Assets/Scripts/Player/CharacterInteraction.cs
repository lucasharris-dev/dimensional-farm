using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInteraction : MonoBehaviour
{
    const string chefTag = "Chef";
    const string blacksmithTag = "Blacksmith";
    const string travelerTag = "Traveler";
    bool isNextToChef = false;
    bool isNextToBlacksmith = false;
    bool isNextToTraveler = false;
    bool canSell = true;

    Keyboard keyboard;
    PlayerGold playerGold;
    MerchantInteractions merchant;
    ChefInteractions chef;
    BlacksmithInteractions blacksmith;
    TravelerInteractions traveler;
    FarmingInventory farmingInventory;


    void Awake()
    {
        keyboard = Keyboard.current;
        playerGold = GetComponent<PlayerGold>();
        farmingInventory = GetComponentInParent<FarmingInventory>();
    }

    void Update()
    {
        Interact();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObject = other.gameObject;

        if (!(otherObject.tag == chefTag || otherObject.tag == blacksmithTag || otherObject.tag == travelerTag))
        {
            return;
        }

        merchant = other.gameObject.GetComponent<MerchantInteractions>();

        switch (other.gameObject.tag)
        {
            case chefTag:
                isNextToChef = true;
                chef = otherObject.GetComponent<ChefInteractions>();
                break;
            case blacksmithTag:
                isNextToBlacksmith = true;
                blacksmith = otherObject.GetComponent<BlacksmithInteractions>();
                break;
            case travelerTag:
                isNextToTraveler = true;
                traveler = otherObject.GetComponent<TravelerInteractions>();
                break;
        }

        Interact();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == chefTag)
        {
            isNextToChef = false;
        }
        if (other.gameObject.tag == blacksmithTag)
        {
            isNextToBlacksmith = false;
        }
        if (other.gameObject.tag == travelerTag)
        {
            isNextToTraveler = false;
        }
    }

    void Interact()
    {
        if (keyboard == null || !keyboard.eKey.isPressed)
        {
            return;
        }

        TalkToNPC();

        if (canSell && isNextToChef)
        {
            SellItem("name", 3); // temp values for testing
            canSell = false; // temp for testing; set back to true once it is confirmed that the player has enough to sell
        }
        else if (isNextToBlacksmith)
        {
            BuyItem("mana crystal", 3);
        }
        else if (isNextToTraveler)
        {
            BuyItem("seed", 3);
        }
    }

    void SellItem(string itemName, int itemCount) // change to give the item's game object
    {
        int profit = chef.BuyFromPlayer(itemName, itemCount);
        playerGold.SetPlayerGold(profit);
    }

    void BuyItem(string itemName, int itemCount) // change to give the item's gameobject
    {
        merchant.SellToPlayer(itemName, itemCount);
    }

    void TalkToNPC()
    {
        Debug.Log("talking to npc");
        
    }
}
