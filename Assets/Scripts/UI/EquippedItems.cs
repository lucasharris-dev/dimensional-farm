using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquippedItems : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemText;
    [SerializeField] TextMeshProUGUI seedText;

    Inventory inventory;

    void Awake()
    {
        inventory = GetComponentInParent<Inventory>(); // not getting
    }

    public void UpdateEquippedItemUI()
    {
        GameObject equippedItem = inventory.GetEquippedItem();
        itemText.text = "Item: " + equippedItem.name.ToString();
    }

    public void UpdateEquippedSeedUI()
    {
        GameObject equippedSeed = inventory.GetEquippedSeed();
        seedText.text = "Seed: " + equippedSeed.name.ToString();
    }

    public void ShowEquippedSeed(bool newValue)
    {
        seedText.enabled = newValue;
    }
}
