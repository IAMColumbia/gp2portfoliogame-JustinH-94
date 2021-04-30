
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManagement : MonoBehaviour
{
    public List<GameObject> numOfUnits = new List<GameObject>();
    public List<GameObject> numOfBuildings = new List<GameObject>();
    public static bool isBaseAttacked;
    public static bool atUnitCap;
    public GameObject System;
    public GameObject Barrack, RadioStation, Base;
    protected const int unitCap = 30;
    protected bool AreAllBuildingsDestroyed;
    protected float buildTimer;
    protected string PlayerName;
    protected string unitTag;

    protected void AllBuildingsDestroyed()
    {
        if (this.AreAllBuildingsDestroyed)
        {
            Debug.Log($"Game Over {PlayerName} wins!");
        }
    }

    protected void UnitCount()
    {
        if(numOfUnits.Count >= unitCap)
        {
            atUnitCap = true;
        }
    }
}
