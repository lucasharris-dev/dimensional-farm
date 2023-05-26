using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantInteractions : MonoBehaviour
{
    BlacksmithInteractions blacksmith;
    TravelerInteractions traveler;


    public void SellToPlayer(string itemName, int numberBought) // replace string with game object; return price
    {
        Debug.Log("sold " + numberBought + " " + itemName + "s for " + (100 * numberBought) + " gold.");
        // add number of items to player inventory
        // get item price (item.GetComponent<ItemInformation>().GetPlayerBuyPrice())
        // check if the items are from blacksmith or traveler, and call their respective interactions class
        // if item from blacksmith
            // blacksmith = GetComponent<BlacksmithInteractions>();
            // blacksmith.GetBlacksmithWares();
        // else if item from traveler
            // traveler = GetComponent<TravelerInteractions>();
            // traveler.GetTravelerWares();
    }
}
