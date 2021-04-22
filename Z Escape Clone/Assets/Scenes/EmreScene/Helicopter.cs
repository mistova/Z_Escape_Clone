using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Helicopter : MonoBehaviour
{
    [SerializeField] PlayableDirector heliDirector;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }

    void TakeOffHelicopter()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
        { heliDirector.Play(); }


    }

    void Update()
    {
        TakeOffHelicopter();
    }
}
