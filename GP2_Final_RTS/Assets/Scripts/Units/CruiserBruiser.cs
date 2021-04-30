using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruiserBruiser : Unit
{
    // Start is called before the first frame update
    protected override void Start()
    {
        name = "CruiserBruiser";
        damageType = "Range";
        moveSpeed = 5.0f;
        rotSpeed = 5.0f;
        attackRange = 15f;
        health = 250;
        attackDamage = 50;
        attackSpeed = 5f;
        buildTime = 6.0f;
        //this.transform.position = new Vector3(spawnLocation.transform.position.x, 20f, spawnLocation.transform.position.z);
        base.Start();
        agent.baseOffset = 11.38f;
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
