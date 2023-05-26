using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelerInteractions : MonoBehaviour
{
    [SerializeField] GameObject beanSeed;
    [SerializeField] GameObject wheatSeed;
    [SerializeField] GameObject cornSeed;
    [SerializeField] GameObject carrotSeed;

    GameObject[] travelerWares;


    void Awake()
    {
        travelerWares = new GameObject[] {beanSeed, wheatSeed, cornSeed, carrotSeed};
    }

    public GameObject[] GetTravelerWares()
    {
        return travelerWares;
    }
}
