using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public enum Buildings
    {
        Stockpile = 0,
        Wall = 1
    }

    public static BuildingController init;

    private void Awake()
    {
        init = this;
    }

    [Header("Temp Stuff")]
    public GameObject stockPile;
    public GameObject wall;

    public Buildings currBuildings;

    public GameObject currentBuildingObj;


    public void changeBuilding(int inB)
    {
        switch (inB)
        {
            case (int)Buildings.Stockpile:
                currBuildings = Buildings.Stockpile;
                currentBuildingObj = stockPile;
                break;
            case (int)Buildings.Wall:
                currBuildings = Buildings.Wall;
                currentBuildingObj = wall;
                break;
        }
    }

}
