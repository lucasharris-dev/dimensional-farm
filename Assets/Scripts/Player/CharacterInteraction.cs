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
    Inventory inventory;
    MerchantInteractions merchant;
    ChefInteractions chef;
    BlacksmithInteractions blacksmith;
    TravelerInteractions traveler;

    void Awake()
    {
        keyboard = Keyboard.current;
        inventory = GetComponent<Inventory>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
    }

    void Interact()
    {
        if (keyboard == null || !keyboard.eKey.isPressed)
        {
            return;
        }

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

    void SellItem(string itemName, int itemCount)
    {
        int profit = 0;
        for (int i = 0; i < itemCount; i++)
        {
            profit += chef.BuyFromPlayer(itemName);
        }
        inventory.SetPlayerMoney(profit); // temp for testing
    }

    void BuyItem(string itemName, int itemCount)
    {
        merchant.SellToPlayer(itemName);
    }
}
