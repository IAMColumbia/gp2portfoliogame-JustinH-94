using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlockingBlock : Unit
{
    public Transform[] defenseLoc;
    int random;
    // Start is called before the first frame update
    protected override void Start()
    {
        random = Random.Range(0, defenseLoc.Length);
        name = "E.BlockingBlock";
        damageType = "Melee";
        moveSpeed = 2.0f;
        attackRange = 12.0f;
        rotSpeed = 2.0f;
        health = 150;
        attackSpeed = 4f;
        attackDamage = 30;
        buildTime = 8.0f;
        base.Start();
        this.transform.position = spawnLocation;
        agent.baseOffset = 0.5f;
    }
    public void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * fovDist, Color.blue, 1, true);
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
        agent.SetDestination(defenseLoc[random].position);
        if (Vector3.Distance(this.transform.position, waitLocation.transform.position) < 5.0f)
        {
            direction = enemyBaseLoc.transform.position - this.transform.position;
            Quaternion lookAt = Quaternion.LookRotation(direction);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookAt, Time.deltaTime * this.rotSpeed);
        }
    }

    void EnemyAtBase()
    {
        if (EnemySystem.isBaseAttacked)
        {
            state = State.defend;
        }
    }

    protected override void ReturnToBase()
    {
        direction = spawnLocation - this.transform.position;
        agent.SetDestination(spawnLocation);

        if (direction.magnitude < 4.0f )
        {
            RandWalk();
        }
    }
}
