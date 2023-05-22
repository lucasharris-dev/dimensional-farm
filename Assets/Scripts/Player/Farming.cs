using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Farming : MonoBehaviour
{
    [SerializeField] GameObject grassPrefab;
    [SerializeField] GameObject farmlandPrefab;
    [SerializeField] GameObject plantedSeedsPrefab;
    [SerializeField] GameObject grownCropsPrefab;

    const string grassTag = "Grass";
    const string farmlandTag = "Farmland";
    const string plantedSeedsTag = "PlantedSeeds";
    const string grownCropsTag = "GrownCrops";
    const string farmingToolTag = "FarmingTool";
    const string seedPouchTag = "SeedPouch";
    const string waterCanTag = "WaterCan";
    const string cropBagTag = "CropBag";

    bool isOnGrass = false;
    bool isOnFarmland = false;
    bool isOnPlantedSeeds = false;
    bool isOnGrownCrops = false;
    bool hasSeeds = true; // set equal to a bool (hasSeeds = (numSeeds > 0) )

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

        switch (other.gameObject.tag)
        {
            case grassTag:
                isOnGrass = true;
                break;
            case farmlandTag:
                isOnFarmland = true;
                break;
            case plantedSeedsTag:
                isOnPlantedSeeds = true;
                break;
            case grownCropsTag:
                isOnGrownCrops = true;
                break;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case grassTag:
                isOnGrass = false;
                break;
            case farmlandTag:
                isOnFarmland = false;
                break;
            case plantedSeedsTag:
                isOnPlantedSeeds = false;
                break;
            case grownCropsTag:
                isOnGrownCrops = false;
                break;
        }
    }

    void Interact()
    {
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
                    WaterCrop();
                    break;
                case cropBagTag:
                    HarvestCrop();
                    break;
            }
        }
    }

    void TillGround()
    {
        if (isOnGrass)
        {
            Instantiate(farmlandPrefab, collidedObject.transform.position, Quaternion.identity); // Q.identity is 0 rotation
            Destroy(collidedObject);
            Debug.Log("till ground");
        }
    }

    void PlantSeed()
    {
        if (isOnFarmland && hasSeeds)
        {
            Instantiate(plantedSeedsPrefab, collidedObject.transform.position, Quaternion.identity);
            Destroy(collidedObject);
            Debug.Log("plant seed");
        }
    }

    void WaterCrop()
    {
        if (isOnPlantedSeeds)
        {
            Instantiate(grownCropsPrefab, collidedObject.transform.position, Quaternion.identity);
            Destroy(collidedObject);
            Debug.Log("water crop");
        }
    }

    void HarvestCrop()
    {
        if (isOnGrownCrops)
        {
            Instantiate(grassPrefab, collidedObject.transform.position, Quaternion.identity);
            Destroy(collidedObject);
            Debug.Log("harvest crop");
        }
    }
}
