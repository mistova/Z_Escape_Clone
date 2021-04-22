using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalAnimations : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        { other.GetComponent<Animator>().Play("Jump"); }
        if (other.CompareTag("Enemy"))
        { Debug.Log("Enemy Fall animasyonu"); }
    }
}
