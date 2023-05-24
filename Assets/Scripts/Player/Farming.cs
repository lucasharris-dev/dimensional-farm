using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Farming : MonoBehaviour
{
    [SerializeField] Tilemap farmTilemap;
    [SerializeField] RuleTile grassRuleTile;
    [SerializeField] RuleTile farmlandRuleTile;
    [SerializeField] RuleTile fertileFarmlandRuleTile;
    [SerializeField] RuleTile plantedFarmlandRuleTile;
    [SerializeField] RuleTile plantedFertileFarmlandRuleTile;

    const string grassTag = "Grass";
    const string farmlandTag = "Farmland";
    const string fertileFarmlandTag = "FertileFarmland";
    const string plantedFarmlandTag = "PlantedFarmland";
    const string plantedFertileFarmlandTag = "PlantedFertileFarmland";
    const string plantedSeedsTag = "PlantedSeeds";
    const string grownCropsTag = "GrownCrops";

    const string farmingToolTag = "FarmingTool";
    const string seedPouchTag = "SeedPouch";
    const string waterCanTag = "WaterCan";
    const string cropBagTag = "CropBag"; // might remove this

    bool useItem = false;

    //bool hasSeeds = true; set equal to a bool (hasSeeds = (numSeeds > 0) ), will be in inventory probably

    Keyboard keyboard;
    Mouse mouse;
    Inventory inventory;
    GameObject collidedObject;

    void Awake()
    {
        keyboard = Keyboard.current;
        mouse = Mouse.current;
        inventory = GetComponent<Inventory>();
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
        if (mouse == null || collidedObject == null || useItem == true)
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
                    PickSeed();
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

    void PickSeed()
    {
        if (keyboard == null || useItem == false)
        {
            return;
        }

        if (keyboard.digit1Key.isPressed)
        {
            PlantSeed("beans");
        }
        if (keyboard.digit2Key.isPressed)
        {
            PlantSeed("wheat");
        }
        if (keyboard.digit3Key.isPressed)
        {
            PlantSeed("corn");
        }
        if (keyboard.digit4Key.isPressed)
        {
            PlantSeed("carrots");
        }
        if (keyboard.digit5Key.isPressed)
        {
            PlantSeed("mana crystal");
        }
        if (keyboard.escapeKey.isPressed)
        {
            // exit menu to choose a seed
        }
    }

    void ReplaceTile(RuleTile newRuleTile)
    {
        Vector3 collidedPosition = collidedObject.transform.position;
        Vector3Int tilePosition = Vector3Int.FloorToInt(collidedPosition);
        farmTilemap.SetTile(tilePosition, newRuleTile);
    }

    void TillGround()
    {
        if (collidedObject.tag == grassTag)
        {
            ReplaceTile(farmlandRuleTile);
            
            Debug.Log("till ground");
        }
    }

    void PlantSeed(string seedName) // change to game object once set up
    {
        if (collidedObject.tag == farmlandTag)
        {
            ReplaceTile(plantedFarmlandRuleTile);
            Debug.Log("plant " + seedName);
        }
        else if (collidedObject.tag == fertileFarmlandTag)
        {
            ReplaceTile(plantedFertileFarmlandRuleTile);
            Debug.Log("plant " + seedName);
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
            inventory.PickUpItem(collidedObject);
            ReplaceTile(grassRuleTile); // temp, need to make it different depending on if the ground is fertilized
            Destroy(collidedObject);
            
            Debug.Log("harvest crop");
        }
    }
}
