using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowCursor : MonoBehaviour
{
    [SerializeField] GameObject defaultCursor;
    [SerializeField] GameObject npcCursor;
    [SerializeField] GameObject miscCursor;
    [SerializeField] GameObject farmingTool;
    [SerializeField] GameObject seedPouch;
    [SerializeField] GameObject waterCan;
    [SerializeField] GameObject cropBag;

    int cursorSprite = 0;

    const string chefTag = "Chef";
    const string blacksmithTag = "Blacksmith";
    const string travelerTag = "Traveler";
    const string boundaryTag = "Boundary";
    const string groundTag = "Ground";
    const string grassTag = "Grass";
    const string farmlandTag = "Farmland";
    const string fertileFarmlandTag = "FertileFarmland";
    const string plantedFarmlandTag = "PlantedFarmland";
    const string plantedFertileFarmlandTag = "PlantedFertileFarmland";
    const string plantedSeedsTag = "PlantedSeeds";
    const string seedTag = "Seed";
    const string cropTag = "Crop";
    const string grownCropsTag = "GrownCrops";

    Mouse mouse;
    Camera mainCamera;
    Inventory inventory;


    void Awake()
    {
        Cursor.visible = false;

        mouse = Mouse.current;
        mainCamera = Camera.main;
        inventory = GetComponentInParent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        CursorFollow();
        ChangeVisibleCursor();
        Debug.Log(cursorSprite);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        string collided = other.gameObject.tag;

        if (collided == chefTag || collided == blacksmithTag || collided == travelerTag)
        {
            cursorSprite = 3;
        }
        else if (collided == grassTag || collided == farmlandTag || collided == fertileFarmlandTag || collided == plantedFarmlandTag || collided == plantedFertileFarmlandTag)
        {
            cursorSprite = 1;
        }
        // else if ()
        // {
        //     // the misc cursor
        // }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        string collided = other.gameObject.tag;

        if (collided == boundaryTag)
        {
            cursorSprite = 0;
        }
    }

    void CursorFollow()
    {
        if (mainCamera == null)
        {
            return;
        }
        Vector3 mousePosition = new Vector3(mouse.position.x.ReadValue(), mouse.position.y.ReadValue(), 10);
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        transform.position = worldPosition;
    }

    void ChangeVisibleCursor()
    {
        DisableAllSpecialCursors();
        switch (cursorSprite)
        {
            case 1: // equipped tool cursor
                Debug.Log(inventory.GetEquippedItem().name);
                inventory.GetEquippedItem().GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 2: // misc interactions cursor
                miscCursor.GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 3: // npc interactions cursor
                npcCursor.GetComponent<SpriteRenderer>().enabled = true;
                npcCursor.GetComponent<Animator>().enabled = true;
                break;
            default: // default cursor
                defaultCursor.GetComponent<SpriteRenderer>().enabled = true;
                break;
        }
    }

    void DisableAllSpecialCursors()
    {
        DisableEquippedToolCursor();
        DisableMiscCursor();
        DisableNPCCursor();
        DisableDefaultCursor();
    }

    void DisableEquippedToolCursor()
    {
        inventory.GetEquippedItem().GetComponent<SpriteRenderer>().enabled = false;
    }

    void DisableMiscCursor()
    {
        miscCursor.GetComponent<SpriteRenderer>().enabled = false;
    }

    void DisableNPCCursor()
    {
        npcCursor.GetComponent<SpriteRenderer>().enabled = false;
        npcCursor.GetComponent<Animator>().enabled = false;
    }

    void DisableDefaultCursor()
    {
        defaultCursor.GetComponent<SpriteRenderer>().enabled = false;
    }
}
