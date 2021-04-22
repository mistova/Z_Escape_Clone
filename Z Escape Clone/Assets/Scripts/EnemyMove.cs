using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Enemyler hareket ediyor, Playerlar kontrol noktasýný geçince en yakýn playerý bularak ona saldýrýyor,

public class EnemyMove : MonoBehaviour
{
    public bool canGo;
    public bool isFinished;
    Transform targetPlayer;
    public NavMeshAgent agent;
    GameObject [] players;
    public Animator anim;
    [SerializeField] GameObject door;



    // Update is called once per frame
    void Update()
    {
        if (door != null)
        {
            if (!door.activeInHierarchy)
                canGo = true;
        }
        else
        {
            canGo = true;
        }


        isFinished = FinishLine.Instance.isFinished;
        players = GameObject.FindGameObjectsWithTag("Player");

        if (canGo && !isFinished && players.Length>0)
        {
            FindClosestPlayer();
            agent.SetDestination(targetPlayer.position);
            anim.SetBool("run", true);
        }

        else if(canGo && !isFinished && players.Length <= 0)
        {
            OyunuBitir();             
        }

        else
        {
            ZombilerBeklesin();
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(BirazSaldir(other.gameObject));            
        }
    }

    void FindClosestPlayer()
    {
        float distanceToClosestPlayer = Mathf.Infinity;
        GameObject closestPlayer = null;      
        

        if (players.Length >0 && !isFinished)
        {
            foreach (var currentPlayer in players)
            {
                float distanceToPlayer = (currentPlayer.transform.position - this.transform.position).sqrMagnitude;
                if (distanceToPlayer < distanceToClosestPlayer)
                {
                    distanceToClosestPlayer = distanceToPlayer;
                    closestPlayer = currentPlayer;
                }
            }

            targetPlayer = closestPlayer.transform;

        }
        else
        {
            canGo = false;            
        }

    }

    IEnumerator BirazSaldir(GameObject other)
    {
        anim.SetTrigger("attack");
        yield return new WaitForSeconds(1);
        FindClosestPlayer();
    }

    void OyunuBitir()
    {
        ZombilerBeklesin();
        UIController.Instance.GameEnd("Game Over");        
    }

    void ZombilerBeklesin()
    {
        targetPlayer = gameObject.transform;
        anim.SetTrigger("idle");        
    }

    
}
