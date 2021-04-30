using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearDaSphere : Unit
{
    // Start is called before the first frame update
    protected override void Start()
    {
        name = "FearDaSphere";
        damageType = "Melee";
        moveSpeed = 3.0f;
        attackRange = 10.0f;
        rotSpeed = 2.0f;
        attackSpeed = 2;
        health = 100;
        attackDamage = 25;
        buildTime = 5.0f;
        //this.transform.position = spawnLocation.transform.position;
        rb = GetComponent<Rigidbody>();
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
