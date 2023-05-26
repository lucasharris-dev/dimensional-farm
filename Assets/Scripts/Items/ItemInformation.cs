using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInformation : MonoBehaviour
{
    [SerializeField] int playerBuyPrice;
    [SerializeField] int playerSellPrice;

    int numberOwned = 0; // once saving player's game, need to get this from save file

    public int GetPlayerBuyPrice()
    {
        if (playerBuyPrice == 0)
        {
            return Mathf.CeilToInt(playerSellPrice * 1.5f);
        }
        else
        {
            return playerBuyPrice;
        }
        
    }

    public int GetPlayerSellPrice()
    {
        return playerSellPrice;
    }

    public int GetNumberOwned()
    {
        return numberOwned;
    }
}
