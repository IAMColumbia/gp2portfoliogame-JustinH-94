using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : SystemManagement
{
    public static List<GameObject> EnemiesInBase = new List<GameObject>();
    public enum State { Prepare, Attack, Defend, Lost}
    public State state;
    public static bool isPrepared;
    bool isBarrackActive;
    bool isDefenceSet;
    float UnitBuildTime =2f;
    private void Start()
    {
        numOfBuildings.Add(Barrack);
        numOfBuildings.Add(RadioStation);
        numOfBuildings.Add(Base);
        isBaseAttacked = false;
        atUnitCap = false;
        state = State.Prepare;
        unitTag = "EnemyUnit";
    }

    private void Update()
    {
        Debug.Log(numOfUnits.Count);
        switch (state)
        {
            case State.Prepare:
                SetUpUnits();
                Ready();
                break;
            case State.Attack:
                break;
            case State.Defend:
                break;
            case State.Lost:
                GameOver();
                break;
        }
        AllBuildingDestroyed();
        UnitAtLimit();
        
    }

    void SetUpUnits()
    {
        buildTimer += Time.deltaTime;
        if (buildTimer > UnitBuildTime && !isDefenceSet)
            SetUpDefenceUnit();
        if (buildTimer > UnitBuildTime && isDefenceSet)
            SetUpOffenseUnit();
    }

    void SetUpDefenceUnit()
    {   
        buildTimer = 0f;
        Barrack.GetComponent<Barracks>().CreateUnit(1);
        if (numOfUnits.Count >= 2)
        {
            isDefenceSet = true;
            UnitBuildTime = 10f;
        }
    }

    void SetUpOffenseUnit()
    {
        buildTimer = 0f;
        Barrack.GetComponent<Barracks>().CreateUnit(0);
    }

    void Ready()
    {
        if (numOfUnits.Count >= 10)
        {
            isPrepared = true;
            state = State.Attack;
        }
    }

    void UnitAtLimit()
    {
        if (numOfUnits.Count >= unitCap)
            atUnitCap = true;
        else
            atUnitCap = false;
    }

    void AllBuildingDestroyed()
    {
        if (numOfBuildings.Count <= 0)
            state = State.Lost;
    }

    void GameOver()
    {
        if (state == State.Lost)
            System.GetComponent<GameOverScript>().GameOver("AI");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerUnit")
        {
            isBaseAttacked = true;
            EnemiesInBase.Add(other.gameObject);

        }
    }
}
