using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class UnitMove : MonoBehaviour
{

    NavMeshAgent agent;
    GameObject selector;
    Vector3 startingPos;
    Vector3 offSet;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        selector = transform.GetChild(0).gameObject;
        selector.SetActive(false);
        startingPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            agent.SetDestination(startingPos);
    }

    public void SelectUnit()
    {
        selector.SetActive(true);
    }

    public void DeSelectUnit()
    {
        selector.SetActive(false);
    }

    public void CalculateOffSet(Vector3 center)
    {
        Vector3 Center = new Vector3(center.x, transform.position.y, center.z);
        offSet = center - transform.position;
    }

    public void MoveToSpot(Vector3 pos)
    {
        agent.ResetPath();
        Vector3 Pos = new Vector3(pos.x, transform.position.y, pos.z);
        Vector3 moveToPos = Pos + offSet;
        agent.SetDestination(moveToPos);
    }

    void MoveToSpot(Vector3 pos, Vector3 center)
    {
        Vector3 Center = new Vector3(center.x, transform.position.y, center.z);
        Vector3 Pos = new Vector3(pos.x, transform.position.y, pos.z);

        offSet = center - transform.position;
        Vector3 moveToPos = Pos + offSet;
        agent.SetDestination(moveToPos);
    }
}
