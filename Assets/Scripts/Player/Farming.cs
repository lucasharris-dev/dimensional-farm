using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Farming : MonoBehaviour
{
    [SerializeField] Tilemap cropTilemap;
    [SerializeField] Tilemap effectTilemap;
    [SerializeField] Tilemap farmTilemap;
    [SerializeField] RuleTile grassRuleTile;
    [SerializeField] RuleTile farmlandRuleTile;
    [SerializeField] RuleTile fertileFarmlandRuleTile;
    [SerializeField] RuleTile plantedFarmlandRuleTile;
    [SerializeField] RuleTile plantedFertileFarmlandRuleTile;
    [SerializeField] RuleTile manaCrystalRuleTile;
    [SerializeField] RuleTile manaCrystalEffect;
    [SerializeField] GameObject manaCrystal;
    [SerializeField] GameObject beanSeed;
    [SerializeField] GameObject wheatSeed;
    [SerializeField] GameObject cornSeed;
    [SerializeField] GameObject carrotSeed;

    const string grassTag = "Grass";
    const string farmlandTag = "Farmland";
    const string fertileFarmlandTag = "FertileFarmland";
    const string plantedFarmlandTag = "PlantedFarmland";
    const string plantedFertileFarmlandTag = "PlantedFertileFarmland";
    const string plantedSeedsTag = "PlantedSeeds";
    const string seedTag = "Seed";
    const string cropTag = "Crop";
    const string grownCropsTag = "GrownCrops";

    const string farmingToolTag = "FarmingTool";
    const string seedPouchTag = "SeedPouch";
    const string waterCanTag = "WaterCan";
    const string cropBagTag = "CropBag"; // might remove this

    bool pickingSeed = false;

    Keyboard keyboard;
    Mouse mouse;
    FarmingInventory farmingInventory;
    EquippedItems equippedItemsScript;
    GameObject collidedObject;


    void Awake()
    {
        keyboard = Keyboard.current;
        mouse = Mouse.current;
        farmingInventory = GetComponentInParent<FarmingInventory>();
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
            switch (farmingInventory.GetEquippedItem().tag)
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
        // if (newRuleTile == manaCrystal)
            // effectTilemap.SetTile(GetCollidedPosition(), manaCrystalEffect)
    }

    void TillGround()
    {
        if (collidedObject.tag == grassTag)
        {
            ReplaceTile(farmlandRuleTile);
            
            Debug.Log("till ground");
        }
    }

    void PlantSeed() // needs to be edited so mana crystals can only be planted on planted seeds
    {
        if (farmingInventory.GetPlayerSeedInventory().Count == 0)
        {
            farmingInventory.GiveOneSeed();
        }

        if (collidedObject.tag == seedTag && (farmingInventory.GetEquippedSeed() == manaCrystal))
        {
            if (collidedObject == carrotSeed)
            {
                collidedObject.GetComponent<SeedInformation>().SetManaFruitQuality(4);
            }
            else if (collidedObject == cornSeed)
            {
                collidedObject.GetComponent<SeedInformation>().SetManaFruitQuality(3);
            }
            else if (collidedObject == wheatSeed)
            {
                collidedObject.GetComponent<SeedInformation>().SetManaFruitQuality(2);
            }
            else if (collidedObject == beanSeed)
            {
                collidedObject.GetComponent<SeedInformation>().SetManaFruitQuality(1);
            }
        }
        else
        {
            if (collidedObject.tag == farmlandTag)
            {
                ReplaceTile(plantedFarmlandRuleTile);

                Debug.Log("plant " + farmingInventory.GetEquippedSeed().name);
                // spawn crop tile
            }
            else if (collidedObject.tag == fertileFarmlandTag)
            {
                ReplaceTile(plantedFertileFarmlandRuleTile);
                
                Debug.Log("plant " + farmingInventory.GetEquippedSeed().name);
                // spawn crop tile
            }
            else if (collidedObject.tag == cropTag)
            {
                if (farmingInventory.GetEquippedSeed() == manaCrystal)
                {
                    SpawnCropTile(manaCrystalRuleTile);
                }
            }
            farmingInventory.RemoveOneSeed(farmingInventory.GetEquippedSeed());
        }
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
            farmingInventory.PickUpItem(collidedObject);
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
