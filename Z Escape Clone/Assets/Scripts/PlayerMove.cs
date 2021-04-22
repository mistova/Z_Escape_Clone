using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform target;
    bool death = false;
    public Animator anim;
    public GameObject zombi;



    // Update is called once per frame
    void Update()
    {
        if (!death)
        {
            agent.SetDestination(target.position);
            anim.SetTrigger("run");
        }

        else
        {
            agent.SetDestination(transform.position);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            death = true;
            anim.SetTrigger("die");
            StartCoroutine(Zombiol());
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            anim.SetTrigger("win");            
            death = false;
        }
    }

    IEnumerator Zombiol()
    {        
        yield return new WaitForSeconds(1.5f);
        GameObject newZombi= Instantiate(zombi, transform.position, Quaternion.identity) as GameObject;
        newZombi.gameObject.GetComponent<EnemyMove>().canGo = true;
        Destroy(this.gameObject);
    }

}
