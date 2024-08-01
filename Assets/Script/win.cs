using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class win : MonoBehaviour
{

    // wwin
    public GameObject window_win;
    bool temp = true;

    private void Update()
    {
        if (gameObject.GetComponent<Monsters>().HP <= 0 && temp)
        {
            Instantiate(window_win, transform.position, Quaternion.identity);
            temp = false;
        }
    }

}
