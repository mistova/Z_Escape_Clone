using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bekleyenler : MonoBehaviour
{
    public PlayerMove playerMove;
    public Animator anim;
    public Canvas canvas;
    public Transform cameraTransform;


    private void Start()
    {
        anim.SetTrigger("idle");
    }
    private void Update()
    {
        canvas.transform.LookAt(cameraTransform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerMove.enabled = true;
            canvas.enabled = false;
            this.gameObject.GetComponent<Bekleyenler>().enabled = false;
        }
    }


}
