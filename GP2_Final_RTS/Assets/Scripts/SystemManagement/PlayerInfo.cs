using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : SystemManagement
{
    public static int unitNum = 0;
    public static List<GameObject> EnemiesInVicinity = new List<GameObject>();
    const int UnitLimit = 20;
    public static bool isAllBuildingDestroyed;
    public static int KillCount;
    // Start is called before the first frame update
    void Start()
    {
        numOfBuildings.Add(Barrack);
        numOfBuildings.Add(RadioStation);
        numOfBuildings.Add(Base);
        unitTag = "PlayerUnit";
    }

    // Update is called once per frame
    void Update()
    {
        UnitOnFloor();
        AllBuildingDestroyed();
        AllEnemiesDestroyed();
        GameOver(); Debug.Log(numOfBuildings.Count);
    }

    void AllEnemiesDestroyed()
    {
        if (EnemiesInVicinity.Count <= 0)
            isBaseAttacked = false;
    }

    void UnitOnFloor()
    {
        if(numOfUnits.Count >= UnitLimit)
        {
            atUnitCap = true;
        }
        else
        {
            atUnitCap = false;
        }
    }

    void AllBuildingDestroyed()
    {
        if (numOfBuildings.Count <= 0)
            isAllBuildingDestroyed = true;
        else
            isAllBuildingDestroyed = false;
    }

    void GameOver()
    {
        if (isAllBuildingDestroyed)
            System.GetComponent<GameOverScript>().GameOver("AI");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="EnemyUnit")
            isBaseAttacked = true;
    }
}
