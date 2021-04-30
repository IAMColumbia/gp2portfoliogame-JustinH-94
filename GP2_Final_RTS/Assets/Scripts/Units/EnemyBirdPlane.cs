using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBirdPlane : Unit
{
    protected override void Start()
    {
        name = "E.BirdPlane";
        damageType = "Range";
        attackSpeed = 1.5f;
        attackRange = 20.0f;
        moveSpeed = 8.0f;
        rotSpeed = 5.0f;
        health = 100;
        attackDamage = 5;
        buildTime = 6.0f;
        //this.transform.position = new Vector3(spawnLocation.transform.position.x, 20f, spawnLocation.transform.position.z);
        base.Start();
        agent.baseOffset = 9.7f;
        this.transform.position = spawnLocation;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * fovDist, Color.red, 1, true);
        switch (state)
        {
            case State.spawn:
                MoveToWaitArea();
                break;
            case State.wait:
                StartSearch();
                break;
            case State.search:
                HeadToEnemyBase();
                break;
            case State.attack:
                AttackTargetUnit();
                break;
            case State.attackBuilding:
                AttackTargetBuilding();
                break;
            case State.retreat:
                break;
            case State.defend:
                ReturnToBase();
                break;
        }
        HealthDetector();
        RemoveBuildingFromList();
        RemoveUnitFromList();
        DontSeeEnemy();
        AttackFromUnitToBuilding();
        SeesEnemy();
        StateAttackBuilding();
        BaseAttacked();
    }
    protected override void MoveToWaitArea()
    {
        base.MoveToWaitArea();
        if (Vector3.Distance(this.transform.position, waitLocation.transform.position) < 10.0f)
            state = State.wait;
    }
}
