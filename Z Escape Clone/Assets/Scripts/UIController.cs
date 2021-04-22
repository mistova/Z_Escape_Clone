using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class UIController : MonoBehaviour
{

    private static UIController instance;
    public static UIController Instance { get => instance; set => instance = value; }


    private void Awake()
    {
        if (instance == null)
            instance = this;     

    }

    [SerializeField]
    TextMeshProUGUI resultText;

    public GameObject gameOverPopupMenu;
    public GameObject[] enemies;
    

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void GameEnd(string result)
    {        
        //kazanan 2 saniye gecikme ile yazdýrýlsýn 
        //StartCoroutine(DelayGameEnd(2, result));
    }


    IEnumerator DelayGameEnd(float time, string result)
    {
        if (enemies.Length > 0)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.gameObject.GetComponent<NavMeshAgent>().isActiveAndEnabled)
                {
                    enemy.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                }                
            }
        }      
        
        yield return new WaitForSeconds(time);
        gameOverPopupMenu.SetActive(true);
        resultText.text = result;
    }

}
