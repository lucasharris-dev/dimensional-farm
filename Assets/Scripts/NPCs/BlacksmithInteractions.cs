using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithInteractions : MonoBehaviour
{
    [SerializeField] GameObject manaCrystal;
    GameObject[] blacksmithWares;

    void Awake()
    {
        blacksmithWares = new GameObject[] {manaCrystal};
    }

    public GameObject[] GetBlacksmithWares()
    {
        return blacksmithWares;
    }
}
