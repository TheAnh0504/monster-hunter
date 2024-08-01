using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private Animator anima;
    // Start is called before the first frame update
    private void Start()
    {
        anima = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Monster")) 
        {
            Die();
        }    
    }

    private void Die()
    {
        anima.SetTrigger("death");
    }
}
