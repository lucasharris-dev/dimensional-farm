using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Farming : MonoBehaviour
{
    Mouse mouse;
    Inventory inventory;

    void Awake()
    {
        mouse = Mouse.current;
        inventory = GetComponent<Inventory>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mouse.leftButton.isPressed)
        {
            Debug.Log("Equipped Item: " + inventory.GetEquippedItem());
        }
    }

    
}
