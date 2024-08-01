using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quai_at : MonoBehaviour
{
    public int st = 10;
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject, 2);
        if (other.CompareTag("Player"))
        {
            other.GetComponent<game_Manager>().TakeDamage(st);
            Destroy(gameObject);
        }

    }
}
