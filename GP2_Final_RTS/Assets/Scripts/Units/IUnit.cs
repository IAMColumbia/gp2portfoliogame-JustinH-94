using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public interface IUnit
{
    Vector3 direction { get; set; }
    Vector3 baseDirection { get; set; }

    NavMeshAgent agent { get; set; }

    Rigidbody rb { get; set; }

    float fovDist { get; set; }
    float attackRange { get; set; }
    float attackSpeed { get; set; }
    float attackTimer { get; set; }
    float timer { get; set; }
    float waitTimer { get; set; }
    float sitAtPos { get; set; }
    float wanderRange { get; set; }
    float buildTime { get; set; }
    float moveSpeed { get; set; }
    float rotSpeed { get; set; }
    
    string damageType { get; set; }

    int health { get; set; }
    int currentHealth { get; set; }
    int attackDamage { get; set; }

    bool isAttacked { get; set; }
}
