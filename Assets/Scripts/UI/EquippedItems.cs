using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquippedItems : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemText;
    [SerializeField] TextMeshProUGUI seedText;

    FarmingInventory farmingInventory;

    void Awake()
    {
        farmingInventory = GetComponentInParent<FarmingInventory>(); // not getting
    }

    public void UpdateEquippedItemUI()
    {
        GameObject equippedItem = farmingInventory.GetEquippedItem();
        itemText.text = "Item: " + equippedItem.name.ToString();
    }

    public void UpdateEquippedSeedUI()
    {
        GameObject equippedSeed = farmingInventory.GetEquippedSeed();
        seedText.text = "Seed: " + equippedSeed.name.ToString();
    }

    public void ShowEquippedSeed(bool newValue)
    {
        seedText.enabled = newValue;
    }
}
