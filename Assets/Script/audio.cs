using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour
{
    [Header("---------------Audio Source-----------")]
    [SerializeField] AudioSource volumeSource;

    [Header("---------------Audio Clip-----------")]
    public AudioClip background;
    // Start is called before the first frame update
    void Start()
    {
        volumeSource.clip = background;
        volumeSource.Play();
    }

    
}
