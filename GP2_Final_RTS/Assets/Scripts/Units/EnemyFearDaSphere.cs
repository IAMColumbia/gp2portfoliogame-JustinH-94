using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyFearDaSphere : Unit
{
    // Start is called before the first frame update
    protected override void Start()
    {
        name = "E.FearDaSphere";
        damageType = "Melee";
        attackRange = 13.0f;
        moveSpeed = 3.0f;
        rotSpeed = 2.0f;
        attackSpeed = 1;
        health = 100;
        attackDamage = 25;
        buildTime = 5.0f;
        rb = GetComponent<Rigidbody>();
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        base.Start();
        this.transform.position = spawnLocation;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Vector3.Distance(this.transform.position, waitLocation.transform.position) <10.0f)
            state = State.wait;
    }
}
