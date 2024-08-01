using StarterAssets;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class bomb : MonoBehaviour
{

    // khai báo âm thanh SFX
    AudioManager audioManager;
   
    public float radius_;

    public GameObject red_;
    public GameObject blue_;
    public GameObject green_;
    public GameObject xanhla_;
    public GameObject do_;


    public GameObject explosionEffectsmall;
    public Animator animaionBomb;

    //dam bom
    public int dambomb;



    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    public void Start()
    {
        audioManager.PlaySFX(audioManager.dat_bom);
        animaionBomb = GetComponent<Animator>();
        animaionBomb.SetTrigger("damage");
        
        PlayerPrefs.SetFloat("x", transform.position.x);
        PlayerPrefs.SetFloat("y", transform.position.y);
        PlayerPrefs.SetFloat("z", transform.position.z);
        PlayerPrefs.SetInt("check", 1);
        StartCoroutine(DestroyAfterDelay(2f));
    }

    // Update is called once per frame
    public void Update()
    {
       
    }

    public System.Collections.IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        
        // khởi tạo vụ nổ
        Instantiate(explosionEffectsmall, transform.position, transform.rotation);
        // SFX vụ nổ
        audioManager.PlaySFX(audioManager.bom);
        // đối tượng va chạm vs vụ nổ
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius_);

        foreach (Collider nearbyObject in colliders)
        {

            if (nearbyObject.CompareTag("Hop"))
            {
                Destroy(nearbyObject.gameObject);

                bool Rand()
                {
                    return Random.value < 0.1f;
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

            if (nearbyObject.CompareTag("Monster"))
            {
                nearbyObject.GetComponent<Monsters>().TakeDamage(dambomb);
            }

            if (nearbyObject.CompareTag("Player"))
            {
                nearbyObject.GetComponent<game_Manager>().TakeDamage(dambomb);

            }
        }

        PlayerPrefs.SetInt("check", 0);
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }


}