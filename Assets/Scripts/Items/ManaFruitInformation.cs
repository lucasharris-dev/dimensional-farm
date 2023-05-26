using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaFruitInformation : MonoBehaviour
{
    int playerSellPrice;

    public int GetPlayerSellPrice()
    {
        return playerSellPrice;
    }

    public void SetPlayerSellPrice(int startingCropSellPrice, int manaFruitQuality)
    {
        playerSellPrice = (startingCropSellPrice * (manaFruitQuality ^ 2));
    }
}
