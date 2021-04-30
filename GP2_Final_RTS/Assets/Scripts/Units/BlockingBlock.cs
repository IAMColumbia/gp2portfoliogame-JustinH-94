using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingBlock : Unit
{
    protected override void Start()
    {
        name = "BlockingBlock";
        damageType = "Melee";
        moveSpeed = 2.0f;
        rotSpeed = 2.0f;
        attackRange = 5.0f;
        health = 150;
        attackSpeed = 4f;
        attackDamage = 40;
        buildTime = 8.0f;
        //this.transform.position = spawnLocation.transform.position;
        base.Start();
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
