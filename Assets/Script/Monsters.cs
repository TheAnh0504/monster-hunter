using JetBrains.Annotations;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Monsters : MonoBehaviour
{
    public int HP = 100;
    public int dam = 10;
    public int thu = 5;
    public Animator animator;
    public Slider healthBar;
    public GameObject fill;

    public GameObject red_;
    public GameObject blue_;
    public GameObject green_;
    public GameObject xanhla_;
    public GameObject do_;
    public game_Manager _player;

    Transform player;

    // bộ đếm time từ lúc quái phát hiện main + score cho quái
    bool chase = true;
    bool chase1 = false;
    private float timer = 0f;
    public int monsterScore = 500;
    public game_Manager main;

    //khai báo âm thanh SFX
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        healthBar.value = HP;

        // người chơi vào tầm nhìn của quái vật
        if (chase)
        {
            if (animator.GetBool("isChasing"))
            {
                chase = false;
                chase1 = true;
            }
        }
        if (chase1)
        {
            timer += Time.deltaTime;
        }
    }

    public void Attack()
    {
        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance <= 2f)
        {
            float Rand1()
            {
                return Random.value;
            }
            if (0f <= Rand1() && Rand1() <= 0.5f)
            {
                audioManager.PlaySFX(audioManager.monster_attack1);
            }
            else if (0.5f < Rand1() && Rand1() <= 1f)
            {
                audioManager.PlaySFX(audioManager.monster_attack2);
            }
            _player.TakeDamage(dam);
        }
    }

    public void Attack1()
    {

        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance <= 2.5f)
        {
            float Rand1()
            {
                return Random.value;
            }
            if (0f <= Rand1() && Rand1() <= 0.5f)
            {
                audioManager.PlaySFX(audioManager.monster_attack1);
            }
            else if (0.5f < Rand1() && Rand1() <= 1f)
            {
                audioManager.PlaySFX(audioManager.monster_attack2);
            }
            _player.TakeDamage(dam);
        }
    }
    public void TakeDamage(int damageAmount)
    {
        // SFX damage
        float Rand2()
        {
            return Random.value;
        }
        if (0f <= Rand2() && Rand2() <= 0.25f)
        {
            audioManager.PlaySFX(audioManager.monster_die);
        }
        else if (0.25f < Rand2() && Rand2() <= 0.5f)
        {
            audioManager.PlaySFX(audioManager.monster_die1);
        }
        else if (0.5f < Rand2() && Rand2() <= 0.75f)
        {
            audioManager.PlaySFX(audioManager.monster_die2);
        }
        else if (0.75f < Rand2() && Rand2() <= 1f)
        {
            audioManager.PlaySFX(audioManager.monster_die3);
        }

        HP = HP - (damageAmount - thu);
        if (HP <= 0 )
        {
            //Play death Animation
            animator.SetTrigger("die");
            GetComponent<Collider>().enabled = false;
            fill.SetActive(false);
            Destroy(gameObject, 3);

            // + score
            chase1 = false;
            int temp = Mathf.FloorToInt((1 - (timer / 150)) * monsterScore);
            if (temp <= Mathf.FloorToInt(monsterScore / 10))
            {
                main.score += Mathf.FloorToInt(monsterScore / 10);
            }
            else
            {
                main.score += temp;
            }

            // rớt item
            bool Rand()
            {
                return Random.value < 0.2f;
            }

            if (Rand())
            {
                float Rand1()
                {
                    return Random.value;
                }

                if (0f <= Rand1() && Rand1() <= 0.2f)
                {
                    Instantiate(red_, transform.position, transform.rotation);
                }
                else if (0.2f < Rand1() && Rand1() <= 0.4f)
                {
                    Instantiate(blue_, transform.position, transform.rotation);
                }
                else if (0.4f < Rand1() && Rand1() <= 0.6f)
                {
                    Instantiate(green_, transform.position, transform.rotation);
                }
                else if (0.6f < Rand1() && Rand1() <= 0.8f)
                {
                    Instantiate(xanhla_, transform.position, transform.rotation);
                }
                else if (0.8f < Rand1() && Rand1() <= 1f)
                {
                    Instantiate(do_, transform.position, transform.rotation);
                }

            }
        } 
        else
        {
            // play get hit animation
            animator.SetTrigger("damage");
            
        }
    }
}
