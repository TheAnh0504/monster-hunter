using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Polyperfect.Universal;
using StarterAssets;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class game_Manager : MonoBehaviour
{
    // green: tốc độ chạy, red: bán kính vụ nổ bomb, blue: mạng
    // list item
    public List<string> items;
    
    // bomb
    public GameObject bomb;
    // điểm của người chơi
    public int score = 0;
    
 
    //khai báo máu, dam, thủ + bán kính nổ bomb
    public int HP = 300;
    public float radius = 1;
    public int thu = 5;
    public ThirdPersonController player;
    public Slider health_Bar;
    // dam
    public GameObject ten;
    public int dam1 = 15;

    // hiển thị trên màn hình 
    
    public Text scoreText;
    public Text damText;
    public Text thuText;
    public Text moveSpeedText;
    public Text radiusText;

    //nổ xong quả bomb này mới đặt đc quả khác
    float timer = 2.5f;
    bool check = true;


    // + điểm khi win
    private float time = 0f;

    //khai báo âm thanh SFX
    AudioManager audioManager;

    /*
    //khai báo âm thanh SFX
    AudioManager audioManager;
    // private void awake
    audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    // SFX vụ nổ
    audioManager.PlaySFX(audioManager.bom);
    */

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        // khai baoos biên lưu vị trí bom
        PlayerPrefs.SetFloat("x", 0f);
        PlayerPrefs.SetFloat("y", 0f);
        PlayerPrefs.SetFloat("z", 0f);

        // tải thông tin người chơi
        if (SceneManager.GetActiveScene().name == "Halloween2")
        {
            dam1 = PlayerPrefs.GetInt("dam1");
            HP = PlayerPrefs.GetInt("HP");
            thu = PlayerPrefs.GetInt("thu");
            radius = PlayerPrefs.GetFloat("radius");
            player.MoveSpeed = PlayerPrefs.GetFloat("moveSpeed");
            score = PlayerPrefs.GetInt("score");
        }
        // hiển thị thông tin người chơi
        moveSpeedText.text = player.MoveSpeed.ToString();
        radiusText.text = radius.ToString();
        damText.text = dam1.ToString();
        thuText.text = thu.ToString();
        scoreText.text = "Score: " + score;

        items = new List<string>();
        ten.GetComponent<Arrow>().dam = dam1;
        bomb.GetComponent<bomb>().dambomb = dam1 * 2;
        bomb.GetComponent<bomb>().radius_ = radius;
    }

    public void TakeDamage(int damageAmount)
    {
        HP = HP - (damageAmount - thu);
        if (HP <= 0)
        {
            //Play death Animation
            player._animator.SetTrigger("die");
            audioManager.PlaySFX(audioManager.death);
            Die();
            
        }
        else
        {
            // play get hit animation
            player._animator.SetTrigger("damage");
            // SFX damage
            float Rand1()
            {
                return Random.value;
            }
            if (0f <= Rand1() && Rand1() <= 0.33f)
            {
                audioManager.PlaySFX(audioManager.damage);
            }
            else if (0.33f < Rand1() && Rand1() <= 0.66f)
            {
                audioManager.PlaySFX(audioManager.damage1);
            }
            else if (0.66f < Rand1() && Rand1() <= 1f)
            {
                audioManager.PlaySFX(audioManager.damage2);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        //die
        if (transform.position.y < -5)
        {
            audioManager.PlaySFX(audioManager.death);
            Die();
        }

        //lưu thông tin người chơi
        PlayerPrefs.SetInt("dam1", dam1);
        PlayerPrefs.SetInt("HP", HP);
        PlayerPrefs.SetInt("thu", thu);
        PlayerPrefs.SetFloat("radius", radius);
        PlayerPrefs.SetFloat("moveSpeed", player.MoveSpeed);
        PlayerPrefs.SetInt("score", score);

        

        //cập nhật hp, dambomb, bán kính boom, dam tên, score
        health_Bar.value = HP;
        ten.GetComponent<Arrow>().dam = dam1;
        bomb.GetComponent<bomb>().dambomb = dam1 * 2;
        bomb.GetComponent<bomb>().radius_ = radius;
        scoreText.text = "Score: " + score;


        // đặt bomb
        if (Input.GetKeyDown(KeyCode.Q) && check)
        {
            check = false;
            Vector3 playerPosition = transform.position;
            Vector3 playerForward = transform.forward;
            Vector3 spawnPosition = playerPosition + playerForward * 1;
            player._animator.SetTrigger("attack");
            Instantiate(bomb, spawnPosition, Quaternion.identity);
        }
        
        if (!check)
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 0)
        {
            check = true;
            timer = 2.5f;
        }

    }

    // ăn items + va chạm cửa win
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            player._animator.SetTrigger("item");
            audioManager.PlaySFX(audioManager.an_item);

            string itemType = other.gameObject.GetComponent<Collectable_script>().itemType;
            print("We can collected a:" + itemType);
            if (itemType == "green")
            {
                player.MoveSpeed = player.MoveSpeed + 1;
                moveSpeedText.text = player.MoveSpeed.ToString();
                score += 100;
            }
            else if (itemType == "blue")
            {
                score += 150;
                radius += 0.5f;
                radiusText.text = radius.ToString();

            }
            else if (itemType == "red")
            {
                HP += 50;
                score += 50;
            }
            else if (itemType == "do")
            {
                score += 200;
                dam1 += 1;
                damText.text = dam1.ToString();
            }
            else if (itemType == "xanhla")
            {
                score += 250;
                thu += 1;
                thuText.text = thu.ToString();
            }
            
            items.Add(itemType);
            print("Inventory length: " + items.Count);
            Destroy(other.gameObject);

        }
        
        else if (other.CompareTag("win_door"))
        {
            if (GameObject.FindGameObjectWithTag("Monster") == null)
            {
                player._animator.SetTrigger("win");
                audioManager.PlaySFX(audioManager.qua_man);
                Destroy(other.gameObject);
                //+ điểm
                int temp = Mathf.FloorToInt((1 - (timer / 600)) * 10000);
                if (temp <= Mathf.FloorToInt(10000 / 10))
                {
                    score += Mathf.FloorToInt(10000 / 10);
                }
                else
                {
                    score += temp;
                }

                StartCoroutine(nextScene(5f));
            }
        }
    }


    //die
    public void Die()
    {
        audioManager.PlaySFX(audioManager.you_lose);
        StartCoroutine(nextLose(2f));
    }


    private System.Collections.IEnumerator nextLose(float delay)
    {
        yield return new WaitForSeconds(delay);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Loose");
          
    }

    public System.Collections.IEnumerator nextScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (SceneManager.GetActiveScene().name == "Halloween2")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("Win");
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    
}
