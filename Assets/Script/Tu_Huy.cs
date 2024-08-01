using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tu_Huy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(DestroyAfterDelay(1f));
    }

    
    private System.Collections.IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Phá hủy script và đối tượng chứa nó
        Destroy(gameObject);
    }
}
