using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    int playerGold; // get from save file
    int lifetimeProfit; // get from save file

    int previousPlayerGold = 0; // temp, will be replaced below by the save file info
    int previousLifetimeProfit = 0; // temp, will be replaced below by the save file info


    void Awake()
    {
        playerGold = previousPlayerGold;
        lifetimeProfit = previousLifetimeProfit;
    }

    public void SetPlayerGold(int amount)
    {
        playerGold += amount;
        lifetimeProfit += playerGold;
    }
}
