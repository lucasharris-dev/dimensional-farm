using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedInformation : MonoBehaviour
{
    [SerializeField] GameObject thisCrop;
    [SerializeField] GameObject manaFruit;
    [SerializeField] float growthTime;

    int manaFruitQuality;

    ItemInformation thisCropInformation;
    ManaFruitInformation manaFruitInformation;

    void Awake()
    {
        thisCropInformation = thisCrop.GetComponent<ItemInformation>();
        manaFruitInformation = manaFruit.GetComponent<ManaFruitInformation>();
        manaFruitQuality = 0;
    }

    GameObject HasGrown()
    {
        if (manaFruitQuality == 0)
        {
            return thisCrop;
        }
        else
        {
            manaFruitInformation.SetPlayerSellPrice(thisCropInformation.GetPlayerSellPrice(), manaFruitQuality);
            return manaFruit;
        }
    }

    public float GetGrowthTime()
    {
        return growthTime;
    }

    public void SetManaFruitQuality(int newValue)
    {
        manaFruitQuality = newValue;
    }
}
