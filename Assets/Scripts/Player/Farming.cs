using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Farming : MonoBehaviour
{
    [SerializeField] Tilemap farmTilemap;
    [SerializeField] Tilemap cropTilemap;
    [SerializeField] RuleTile grassRuleTile;
    [SerializeField] RuleTile farmlandRuleTile;
    [SerializeField] RuleTile fertileFarmlandRuleTile;
    [SerializeField] RuleTile plantedFarmlandRuleTile;
    [SerializeField] RuleTile plantedFertileFarmlandRuleTile;
    [SerializeField] RuleTile manaCrystalRuleTile;
    [SerializeField] GameObject manaCrystal;

    const string grassTag = "Grass";
    const string farmlandTag = "Farmland";
    const string fertileFarmlandTag = "FertileFarmland";
    const string plantedFarmlandTag = "PlantedFarmland";
    const string plantedFertileFarmlandTag = "PlantedFertileFarmland";
    const string plantedSeedsTag = "PlantedSeeds";
    const string cropTag = "Crop";
    const string grownCropsTag = "GrownCrops";

    const string farmingToolTag = "FarmingTool";
    const string seedPouchTag = "SeedPouch";
    const string waterCanTag = "WaterCan";
    const string cropBagTag = "CropBag"; // might remove this

    bool pickingSeed = false;

    Keyboard keyboard;
    Mouse mouse;
    Inventory inventory;
    EquippedItems equippedItemsScript;
    GameObject collidedObject;

    void Awake()
    {
        keyboard = Keyboard.current;
        mouse = Mouse.current;
        inventory = GetComponent<Inventory>();
        equippedItemsScript = GetComponentInChildren<EquippedItems>();
    }

    void Update()
    {
        Interact();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        collidedObject = other.gameObject;
    }

    void Interact()
    {
        if (mouse == null || collidedObject == null || pickingSeed == true)
        {
            return;
        }

        if (mouse.leftButton.isPressed)
        {
            switch (inventory.GetEquippedItem().tag)
            {
                case farmingToolTag:
                    TillGround();
                    break;
                case seedPouchTag:
                    PlantSeed();
                    break;
                case waterCanTag:
                    WaterFarmland();
                    break;
                case cropBagTag:
                    HarvestCrop();
                    break;
            }
        }
    }

    Vector3Int GetCollidedPosition()
    {
        Vector3 collidedPosition = collidedObject.transform.position;
        return Vector3Int.FloorToInt(collidedPosition);
    }

    void ReplaceTile(RuleTile newRuleTile)
    {
        farmTilemap.SetTile(GetCollidedPosition(), newRuleTile);
    }

    void SpawnCropTile(RuleTile newRuleTile)
    {
        cropTilemap.SetTile(GetCollidedPosition(), newRuleTile);
    }

    void TillGround()
    {
        if (collidedObject.tag == grassTag)
        {
            ReplaceTile(farmlandRuleTile);
            
            Debug.Log("till ground");
        }
    }

    void PlantSeed() // needs to be edited so mana crystals can only be planted 
    {
        if (inventory.GetPlayerSeedInventory().Count == 0)
        {
            inventory.GiveOneSeed();
        }
        
        if (collidedObject.tag == farmlandTag)
        {
            ReplaceTile(plantedFarmlandRuleTile);

            Debug.Log("plant " + inventory.GetEquippedSeed().name);
        }
        else if (collidedObject.tag == fertileFarmlandTag)
        {
            ReplaceTile(plantedFertileFarmlandRuleTile);
            Debug.Log("plant " + inventory.GetEquippedSeed().name);
        }
        else if (collidedObject.tag == cropTag)
        {
            if (inventory.GetEquippedSeed() == manaCrystal)
            {
                SpawnCropTile(manaCrystalRuleTile);
            }
        }
        // remove 1 of a seed
    }

    void WaterFarmland()
    {
        if (collidedObject.tag == farmlandTag)
        {
            ReplaceTile(fertileFarmlandRuleTile);

            Debug.Log("water crop");
        }
        else if (collidedObject.tag == plantedFarmlandTag)
        {
            ReplaceTile(plantedFertileFarmlandRuleTile);
            
            Debug.Log("water crop");
        }
    }

    void HarvestCrop()
    {
        if (collidedObject.tag == grownCropsTag)
        {
            inventory.PickUpItem(collidedObject);
            ReplaceTile(grassRuleTile); // temp, need to make it different depending on if the ground is fertilized
            Destroy(collidedObject);
            
            Debug.Log("harvest crop");
        }
    }

    public bool GetPickingSeed()
    {
        return pickingSeed;
    }

    public void SetPickingSeed(bool newValue)
    {
        pickingSeed = newValue;
    }
}
