using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefInteractions : MonoBehaviour
{
    public int BuyFromPlayer(string itemName, int numberSold) // replace itemName with the item gameObject
    {
        // remove numberSold of the item from the player
        return (100 * numberSold);// replace 100 with the actual value
    }
}
