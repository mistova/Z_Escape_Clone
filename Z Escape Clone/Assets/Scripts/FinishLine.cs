using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private static FinishLine instance;
    public static FinishLine Instance { get => instance; set => instance = value; }

    public bool isFinished;

    private void Awake()
    {
        if (instance == null)
            instance = this;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isFinished= true;    
            other.gameObject.name = "winner";
            UIController.Instance.GameEnd("You Won");  
        }
    }
}
