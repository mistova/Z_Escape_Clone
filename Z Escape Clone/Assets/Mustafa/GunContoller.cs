using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunContoller : MonoBehaviour
{
    private static GunContoller instance;
    public static GunContoller Instance { get => instance; set => instance = value; }

    [SerializeField]
    RectTransform cross;
    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    ParticleSystem fireEffect;
    [SerializeField]
    float attackCoolDown;
    [SerializeField]
    GameObject bulletHolePref;
    float waitForAttack;

    internal float GetGunKickTime()
    {
        return attackCoolDown;
    }

    bool kickControl;

    Vector2 firstPosition, tempPosition;

    bool onClick;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        waitForAttack = 0;
        onClick = false;
        kickControl = false;
        firstPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        tempPosition = Vector2.zero;
    }

    void Update()
    {
        //Debug.DrawRay(transform.position, transform.position + transform.forward.normalized * 1000, Color.yellow);
        GunMoves();
        if (isMouseOnScreen())
        {
            if (Input.GetMouseButtonDown(0))
            {
                tempPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                onClick = true;
                //Debug.Log("X: " + firstPosition.x);
                //Debug.Log("Y: " + firstPosition.y);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                onClick = false;
                firstPosition = cross.position;
            }
            else if (onClick)
            {
                SetCrossHair();
                Shoot();
            }
        }
        else
            onClick = false;
    }

    private void GunMoves()
    {
        waitForAttack += Time.deltaTime;
    }

    private void SetCrossHair()
    {
        Vector2 mouseDifference = tempPosition - new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 pos = firstPosition - mouseDifference;
        if (pos.x < 0)
            pos.x = 0;
        else if (pos.x > Screen.width)
            pos.x = Screen.width;
        if (pos.y < 0)
            pos.y = 0;
        else if (pos.y > Screen.height)
            pos.y = Screen.height;
        cross.position = pos;
    }

    private void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(cross.position);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
            transform.LookAt(hit.point);
            if (waitForAttack > attackCoolDown)
            {
                waitForAttack = 0;
                fireEffect.Play();
                GetComponent<Animator>().SetTrigger("Shoot");
                if (hit.transform.gameObject.CompareTag("Enemy"))
                {
                    // vedat ekliyor                    
                   hit.transform.gameObject.GetComponent<EnemyMove>().enabled = false;
                   hit.transform.gameObject.GetComponent<ZombiYokOl>().enabled = true;
                    //Destroy(hit.transform.gameObject);
                }
                else if (hit.transform.gameObject.CompareTag("Bomb"))
                {
                    hit.transform.gameObject.GetComponent<BombExplode>().Explode();
                }
                else if(hit.transform.gameObject.CompareTag("Ground"))
                    Instantiate(bulletHolePref, hit.point, new Quaternion(0,0,0,0));
            }
        }
    }

    bool isMouseOnScreen()
    {
        if (Input.mousePosition.x > Screen.width || Input.mousePosition.y > Screen.height || Input.mousePosition.x < 0 || Input.mousePosition.y < 0)
            return false;
        return true;
    }
}
