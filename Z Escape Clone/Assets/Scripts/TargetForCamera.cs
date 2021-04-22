using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//playerlarýn ortalamasý alýnýyor, kamera onu takip ediyor;

public class TargetForCamera : MonoBehaviour
{
    GameObject[] players;
    GameObject[] enemies;    

      
    // Update is called once per frame
    void Update()
    {
        //sahnede playerlar varsa onlarý takip ediyoruz, yoksa zombileri
        players = GameObject.FindGameObjectsWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (players.Length>0)
        {
            transform.position = GetAvaragePosition(players);
        }
        else 
        {
            transform.position = GetAvaragePosition(enemies);
        } 
 
    }


    Vector3 GetAvaragePosition(GameObject [] group)
    {
        Vector3 vec = Vector3.zero;       

        foreach (var person in group)
        {
            vec += person.transform.position;  
        }

        return vec / group.Length;
    }

}
