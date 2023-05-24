using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowCursor : MonoBehaviour
{
    string itemsTag = "Items";

    Mouse mouse;
    Camera mainCamera;
    GameObject items;

    void Awake()
    {
        mouse = Mouse.current;
        mainCamera = Camera.main;
        items = GameObject.FindGameObjectWithTag(itemsTag);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera == null)
        {
            return;
        }
        Vector3 mousePosition = new Vector3(mouse.position.value.x, mouse.position.value.y, -10f);
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(mousePosition);
        items.transform.position = screenPosition;
        Debug.Log(screenPosition);
    }
}
