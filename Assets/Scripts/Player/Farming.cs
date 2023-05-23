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
    [SerializeField] RuleTile plantedFarmlandRuleTile;
    [SerializeField] RuleTile fertileFarmlandRuleTile;
    [SerializeField] RuleTile fertilePlantedFarmlandRuleTile;

    [SerializeField] GameObject grassPrefab;
    [SerializeField] GameObject farmlandPrefab;
    [SerializeField] GameObject plantedSeedsPrefab;
    [SerializeField] GameObject grownCropsPrefab;

    const string grassTag = "Grass";
    const string farmlandTag = "Farmland";
    const string plantedFarmlandTag = "PlantedFarmland";
    const string fertileFarmlandTag = "FertileFarmland";
    const string fertilePlantedFarmlandTag = "FertilePlantedFarmland";
    const string plantedSeedsTag = "PlantedSeeds";
    const string grownCropsTag = "GrownCrops";

    const string farmingToolTag = "FarmingTool";
    const string seedPouchTag = "SeedPouch";
    const string waterCanTag = "WaterCan";
    const string cropBagTag = "CropBag";

    //bool hasSeeds = true; set equal to a bool (hasSeeds = (numSeeds > 0) ), will be in inventory probably

    Mouse mouse;
    Inventory inventory;
    GameObject collidedObject;

    void Awake()
    {
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
        if (mouse == null || collidedObject == null)
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

    void PlantSeed()
    {
        if (collidedObject.tag == farmlandTag || collidedObject.tag == fertileFarmlandTag)
        {
            ReplaceTile(plantedFarmlandRuleTile);
            Debug.Log("plant seed");
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
            ReplaceTile(fertilePlantedFarmlandRuleTile);
            
            Debug.Log("water crop");
        }
    }

    void HarvestCrop()
    {
        if (collidedObject.tag == grownCropsTag)
        {
            ReplaceTile(grassRuleTile); // temp, need to make it different depending on if the ground is fertilized
            
            Debug.Log("harvest crop");
        }
    }
}
