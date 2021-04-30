using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public enum State
    {
        spawn, wait, search, attack, retreat, defend, attackUnit, attackBuilding, moving
    }

    protected Vector3 direction, baseDirection;
    protected List<GameObject> targetUnits = new List<GameObject>();
    protected List<GameObject> targetBuildings = new List<GameObject>();
    protected NavMeshAgent agent;
    protected float fovDist = 50.0f;
    protected float attackRange;
    protected float attackTimer;
    protected float attackSpeed;
    protected float fovAngle = 90.0f;
    protected float timer;
    protected float waittimer;
    protected float sitAtPos = 2.0f;
    protected float range = 10.0f;
    protected Rigidbody rb;
    public State state;
    protected string unitName;
    protected string damageType;
    protected float buildTime;
    protected int health;
    public int currentHealth;
    protected int attackDamage;
    protected int counter;
    public GameObject currentTarget;
    
    protected bool isReadyToAttack;
    protected bool seesEnemy;
    protected bool seesBuilding;
    protected RaycastHit hit;

    public float moveSpeed;
    public float rotSpeed;
    public GameObject buildingCreatedAt;
    protected Vector3 spawnLocation;
    public GameObject waitLocation;
    public GameObject enemyBaseLoc;
    public LayerMask layerMask;
    public List<GameObject> EnemyUnits;
    public List<GameObject> EnemyBuildings;
    public bool isPlayer;
    public bool isAttacked;
    public GameObject attackingUnit;
    protected virtual void Start()
    {
        state = State.spawn;
        agent = GetComponent<NavMeshAgent>();
        spawnLocation = GameObject.Find(buildingCreatedAt.name).GetComponent<Building>().SpawnLoc.transform.position;
        this.transform.position = new Vector3(spawnLocation.x +15f, spawnLocation.y, spawnLocation.z);
        agent.enabled = false;
        GetComponent<TakeDamage>().SetHealth(health);
        agent.baseOffset = 1f;
        Invoke("EnableNavMeshAgent", 0.025f);
    }

    protected void CheckForNullGO()
    {
        if(EnemyUnits.Count > 0)
        {
            if(EnemyUnits[0] == null)
            {
                EnemyUnits.RemoveAt(0);
            }
        }
    }

    protected void EnableNavMeshAgent()
    {
        agent.enabled = true;
    }

    protected virtual void MoveToWaitArea()
    {
        agent.SetDestination(waitLocation.transform.position);

        if (Vector3.Distance(this.transform.position, waitLocation.transform.position) < 5.0f)
            state = State.wait;
    }

    protected virtual void StartSearch()
    {
        if(GameObject.Find("EnemySystem").GetComponent<EnemySystem>().numOfUnits.Count >=10)
        {
            this.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
            this.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();
            state = State.search;
        }
    }

    protected void HeadToEnemyBase()
    {
        baseDirection = enemyBaseLoc.transform.position - this.transform.position;

        if (baseDirection.magnitude < 10f)
            state = State.attackBuilding;
        else
        {
            agent.SetDestination(enemyBaseLoc.transform.position);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(enemyBaseLoc.transform.position), Time.deltaTime * this.rotSpeed);
        }
    }

    protected void RandWalk()
    {
        timer += Time.deltaTime;

        if (timer >= sitAtPos)
        {
            Vector3 newPos = RandomNavSphere(transform.position, range, -1); //sets new position for the agent to go to from within the nav mesh
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    protected Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDir = Random.insideUnitSphere * dist;

        randDir += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDir, out navHit, dist, layermask);

        return navHit.position;
    }

    protected virtual void SeeEnemy()
    {
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, fovDist, ~layerMask))
        {
            if ((hit.collider.gameObject.tag == "EnemyUnit"))
            {
                currentTarget = hit.collider.gameObject;
                state = State.attackUnit;
            }
        }
    }

    protected void StateAttackBuilding()
    {
        if (targetUnits.Count <= 0 && targetBuildings.Count > 0)
            state = State.attackBuilding;
    }

    protected void RemoveBuildingFromList()
    {
        for(int i = 0; i < targetBuildings.Count; i++)
        {
            if (targetBuildings[i].GetComponent<TakeDamage>().Health <= 0)
                targetBuildings.RemoveAt(i);
        }
    }

    protected void RemoveUnitFromList()
    {
        for (int i = 0; i < targetUnits.Count; i++)
        {
            if (targetUnits[i].GetComponent<TakeDamage>().Health <= 0)
                targetUnits.RemoveAt(i);
        }
    }

    protected void SeesEnemy()
    {
        if (targetUnits.Count > 0 && state != State.moving && state != State.defend)
            state = State.attack;
    }

    protected void DontSeeEnemy()
    {
        if (targetUnits.Count == 0)
        {
            seesEnemy = false;
            //target = new List<GameObject>();
        }
        if (targetBuildings.Count == 0)
            seesBuilding = false;
    }

    protected void AttackFromUnitToBuilding()
    {
        if (targetUnits.Count <= 0 && targetBuildings.Count > 0 && state != State.moving && state != State.defend)
            state = State.attackBuilding;
    }

    protected void AttackTargetBuilding()
    {
        if (seesBuilding)
        {
            this.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
            this.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();
            GameObject firstTarget = targetBuildings[0];
            direction = firstTarget.transform.position - this.transform.position;
            float angle = Vector3.Angle(direction, this.transform.position);
            if (direction.magnitude < attackRange)
            {
                Attack(firstTarget);
            }
            else
            {
                this.transform.Translate(0, 0, moveSpeed * Time.deltaTime);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * this.rotSpeed);
            }
        }
    }

    protected void AttackTargetUnit()
    {
        if (seesEnemy)
        {
            this.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
            this.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();
            GameObject firstTarget = targetUnits[0];
            direction = firstTarget.transform.position - this.transform.position;
            float angle = Vector3.Angle(direction, this.transform.position);
            if (direction.magnitude < attackRange)
            {
                Attack(firstTarget);
            }
            else
            {
                this.transform.Translate(0, 0, moveSpeed * Time.deltaTime);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * this.rotSpeed);
            }
        }
    }

    protected void Attack(GameObject _target)
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackSpeed)
        {
            
            _target.GetComponent<TakeDamage>().DamageTaken(attackDamage, this.gameObject);
            attackTimer = 0;
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.tag == "PlayerUnit")
        {
            if (other.gameObject.tag == "EnemyUnit")
            {
                seesEnemy = true;
                targetUnits.Add(other.gameObject);
                state = State.attack;
            }
            else if (other.gameObject.tag == "EnemyBase")
            {
                targetBuildings.Add(other.gameObject);
                seesBuilding = true;
            }
        }
        else if(this.gameObject.tag == "EnemyUnit")
        {
            if (other.gameObject.tag == "PlayerUnit")
            {
                seesEnemy = true;
                targetUnits.Add(other.gameObject);
                state = State.attack;
            }
            else if (other.gameObject.tag == "PlayerBase")
            {
                targetBuildings.Add(other.gameObject);
                seesBuilding = true;
            }
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (this.gameObject.tag == "PlayerUnit")
        {
            if (other.gameObject.tag == "EnemyUnit")
                targetUnits.Remove(other.gameObject);
            else if (other.gameObject.tag == "EnemyBase")
                targetBuildings.Remove(other.gameObject);
        }
        else if (this.gameObject.tag == "EnemyUnit")
        {
            if (other.gameObject.tag == "PlayerUnit")
                targetUnits.Remove(other.gameObject);
            else if (other.gameObject.tag == "PlayerBase")
                targetBuildings.Add(other.gameObject);
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        if(other.gameObject == null)
        {
            targetBuildings.Clear();
            targetUnits.Clear();
        }
    }

    protected virtual void DefendBase()
    {
        if(EnemySystem.EnemiesInBase[0].GetComponent<TakeDamage>().Health <= 0)
        {
            EnemySystem.EnemiesInBase.RemoveAt(0);
        }
        Vector3 TargetPosition = EnemySystem.EnemiesInBase[0].transform.position - this.transform.position;
        if(TargetPosition.magnitude < attackRange)
        {
            Attack(EnemySystem.EnemiesInBase[0]);
        }
        else
        {
            this.transform.Translate(0, 0, moveSpeed * Time.deltaTime);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(TargetPosition), Time.deltaTime * this.rotSpeed);
            waittimer = 0;
        }
    }

    protected virtual void ReturnToBase()
    {
        direction = waitLocation.transform.position - this.transform.position;

        if (direction.magnitude < 10.0f)
        {
            waittimer += Time.deltaTime;
            if(waittimer >= 5f)
            {
                state = State.search;
            }
            DefendBase();
        }
        else
        {
            agent.SetDestination(waitLocation.transform.position);
        }
    }



    protected void HealthDetector()
    {
        if(health <= 0)
            Destroy(this.gameObject);
    }

    protected void BaseAttacked()
    {
        if (EnemySystem.isBaseAttacked)
        {
            state = State.defend;
        }
    }
}
