using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseSystem : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] string targetTag;
    [SerializeField] GameObject door;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!door.activeInHierarchy)
        FindClosestTarget();
    }

    public void FindClosestTarget()
    {

        GameObject[] targets;
        targets = GameObject.FindGameObjectsWithTag(targetTag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject target in targets)
        {
            Vector3 diff = target.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = target;
                distance = curDistance;
            }
        }
        if (closest != null) { agent.SetDestination(closest.transform.position); }
    }
}
