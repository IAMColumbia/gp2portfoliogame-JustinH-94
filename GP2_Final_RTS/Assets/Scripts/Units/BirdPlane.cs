using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPlane : Unit
{
    protected override void Start()
    {
        name = "BirdPlane";
        damageType = "Range";
        attackSpeed = 1.5f;
        attackRange = 10.0f;
        moveSpeed = 8.0f;
        rotSpeed = 5.0f;
        health = 100;
        attackDamage = 5;
        buildTime = 6.0f;
        //this.transform.position = new Vector3(spawnLocation.transform.position.x, 20f, spawnLocation.transform.position.z);
        base.Start();
        agent.baseOffset = 9.7f;
    }


    // Update is called once per frame
    public void Update()
    {
        Debug.Log(state);
        Debug.Log(targetBuildings.Count);
        Debug.Log(targetUnits.Count);
        switch (state)
        {
            case State.attack:
                AttackTargetUnit();
                break;
            case State.attackBuilding:
                AttackTargetBuilding();
                break;
            case State.moving:
                break;
        }
        HealthDetector();
        RemoveBuildingFromList();
        RemoveUnitFromList();
        DontSeeEnemy();
        AttackFromUnitToBuilding();
        SeesEnemy();
        StateAttackBuilding();
    }
}
