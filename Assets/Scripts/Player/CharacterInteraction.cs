using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInteraction : MonoBehaviour
{
    string merchantTag = "Merchant";
    bool isNextToMerchant = false;
    bool canSell = true;

    Keyboard keyboard;
    Inventory inventory;
    MerchantInventory merchant;

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
        if (other.gameObject.tag == merchantTag)
        {
            isNextToMerchant = true;
            merchant = other.gameObject.GetComponent<MerchantInventory>();
            Interact();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == merchantTag)
        {
            isNextToMerchant = false;
        }
    }

    void Interact()
    {
        if (canSell && isNextToMerchant && keyboard.eKey.isPressed)
        {
            SellItem("name", 3); // temp values for testing
            canSell = false; // temp for testing; set back to true once it is confirmed that the player has enough to sell
        }
    }

    void SellItem(string itemName, int itemCount)
    {
        int profit = 0;
        for (int i = 0; i < itemCount; i++)
        {
            profit += merchant.BuyItem(itemName);
        }
        inventory.SetPlayerMoney(profit); // temp for testing
    }
}
