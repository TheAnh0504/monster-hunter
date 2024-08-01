using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // dam của main
    public int dam;
    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject, 2);
        if (!other.CompareTag("Player"))
        {
            Destroy(transform.GetComponent<Rigidbody>());
            if(other.tag == "Monster")
            {
                transform.parent = other.transform;
                other.GetComponent<Monsters>().TakeDamage(dam);
            }
        }
            
    }
}
