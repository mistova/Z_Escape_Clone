using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplode : MonoBehaviour
{
    [SerializeField]
    GameObject explodeEffectPref;
    [SerializeField]
    float explosionR;
    [SerializeField]
    LayerMask layerMask;
    internal void Explode()
    {
        Instantiate(explodeEffectPref, transform.position, transform.rotation);
        Collider [] collideds = Physics.OverlapSphere(transform.position, explosionR, layerMask);
        foreach(var obj in collideds)
        {
            //Destroy(obj.gameObject);
            //VEDAT EKLÝYOR
            if (obj.CompareTag("Enemy"))
            {
                obj.GetComponent<EnemyMove>().enabled = false;
                obj.GetComponent<ZombiYokOl>().enabled = true;
            }
        }
        Destroy(gameObject);
    }
}
