using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header ("---------------Audio Source-----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------------Audio Clip-----------")]
    public AudioClip background;
    public AudioClip backgroundLose;
    public AudioClip backgroundWin;
    public AudioClip qua_man;
    public AudioClip you_lose;
    [Header("---------------Main-----------")]
    public AudioClip death;
    public AudioClip attack;
    public AudioClip damage;
    public AudioClip damage1;
    public AudioClip damage2;
    public AudioClip ban_ten;
    public AudioClip bom;
    public AudioClip dat_bom;
    public AudioClip an_item;
    [Header("---------------Monster-----------")]
    public AudioClip monster_attack1;
    public AudioClip monster_attack2;
    public AudioClip monster_die;
    public AudioClip monster_die1;
    public AudioClip monster_die2;
    public AudioClip monster_die3;

    public static AudioManager instance;
    private void Awake()
    {
        if(SceneManager.GetActiveScene().name != "Start" &&
            SceneManager.GetActiveScene().name != "Loose" &&
            SceneManager.GetActiveScene().name != "Win")
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                
            }
        }
        
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Win")
        {
            musicSource.clip = backgroundWin;
            musicSource.Play();
        }
        else if (SceneManager.GetActiveScene().name == "Loose")
        {
            musicSource.clip = backgroundLose;
            musicSource.Play();
        }
        else if (SceneManager.GetActiveScene().name == "Start")
        {
            Destroy(gameObject);
        } else
        {
            musicSource.clip = background;
            musicSource.Play();
        }
        

    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Start" ||
            SceneManager.GetActiveScene().name == "Loose" ||
            SceneManager.GetActiveScene().name == "Win")
        {
            Destroy(gameObject);
        }
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
