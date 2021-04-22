using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class ZombiYokOl : MonoBehaviour
{
    public Transform gun;
    public Renderer skin;
    public GameObject puan;



    private Vector3 yatay = new Vector3(-90f,0,0f);
    private float smooth = 3f;

    private void Update()
    {
        GameObject gosterilenPuan = Instantiate(puan, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity) as GameObject;
        gosterilenPuan.transform.LookAt(gun);
        skin.material.color = Color.black;
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        Quaternion oluPos = Quaternion.Euler(yatay);
        transform.rotation = Quaternion.Lerp(transform.rotation,oluPos, Time.deltaTime * smooth);
        Destroy(gosterilenPuan,0.5f);
        Destroy(this.gameObject, 1f);

    }


}
