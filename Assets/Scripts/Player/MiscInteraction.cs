using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiscInteraction : MonoBehaviour
{
    [SerializeField] GameObject ManaCrystal;

    Inventory inventory;
    FollowCursor playerCursor;
    Mouse mouse;

    bool isOnItem = false;

    void Awake()
    {
        inventory = GetComponentInParent<Inventory>();
        playerCursor = GetComponent<FollowCursor>();
        mouse = Mouse.current;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (mouse == null || other == null)
        {
            return;
        }

        GameObject collided = other.gameObject;
        if (mouse.leftButton.isPressed)
        {
            if (collided.name == ManaCrystal.name)
            {
                inventory.PickUpItem(collided);
                Destroy(collided);
                playerCursor.SetCursorSprite(0);
            }
        }
    }

    // check if hovering over an item, and if the player left clicks
        // if they do, reset the cursor
        // if they leave the collision, reset the cursor
}
