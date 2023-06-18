using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowCursor : MonoBehaviour
{
    [SerializeField] GameObject items;

    string itemsTag = "Items";

    Mouse mouse;
    Camera mainCamera;


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
        Vector3 mousePosition = new Vector3(mouse.position.x.ReadValue(), mouse.position.y.ReadValue(), 10);
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        items.transform.position = worldPosition;
    }
}
