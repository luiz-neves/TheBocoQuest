using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class bossScript1 : MonoBehaviour
{
    public bool inDialog = true;
    public bool isTp = false;
    public float tpTime = 5;
    public float shotTime = 2f;
    private float tpTimer = 0;
    private float shotTimer = 0;
    public GameObject fireBall;
    private GameObject playerGo;
    private bool s2 = false;
    public AudioClip spellSound;
    public AudioClip tpSound;

    void Start()
    {
        playerGo = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (!inDialog)
        {
            shotTimer += 0.02f;
            if (!isTp && shotTimer > shotTime)
            {
                int dir = (int)Mathf.Sign(transform.position.x - playerGo.transform.position.x);
                Vector3 target = playerGo.transform.position - transform.position;
                float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
                Instantiate(fireBall, new Vector3(transform.position.x + (transform.localScale.x / 14 * -dir), transform.position.y), Quaternion.Euler(0, 0, angle)); //new Quaternion (0, 0, ((dir == 1) ? 180 : 0), 0)
                shotTimer = 0;
                GetComponent<AudioSource>().PlayOneShot(spellSound);
            }

            if (tpTimer > tpTime)
            {
                isTp = true;
                if (this.transform.localScale.x > 1 && s2 == false)
                {
                    this.transform.localScale = new Vector3(transform.localScale.x - 0.2f, transform.localScale.y - 0.2f, 1);
                }
                else
                {
                    if (this.GetComponent<SpriteRenderer>().flipX && this.transform.localScale.x > 1)
                    {
                        this.transform.position = new Vector3(-12f, -0.25f, 0f);
                        GetComponent<AudioSource>().PlayOneShot(tpSound);
                    }
                    else
                    {
                        this.transform.position = new Vector3(11.5f, -0.25f, 0f);
                        GetComponent<AudioSource>().PlayOneShot(tpSound);
                    }
                    s2 = true;
                    if (this.transform.localScale.x < 15)
                    {
                        this.transform.localScale = new Vector3(transform.localScale.x + 0.3f, transform.localScale.y + 0.3f, 1);
                    }
                    else
                    {
                        this.GetComponent<SpriteRenderer>().flipX = !this.GetComponent<SpriteRenderer>().flipX;
                        this.transform.localScale = new Vector3(15, 15, 1);
                        isTp = false;
                        tpTimer = 0;
                        s2 = false;
                        shotTimer = shotTime;
                    }
                }

            }
            tpTimer += 0.02f;
        }
    }


    void OnTriggerEnter2D(Collider2D colisor)
    {
        if (colisor.gameObject.tag == "GroundCheck" && !isTp)
        {
            playerGo.GetComponent<player>().currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(playerGo.GetComponent<player>().currentLevel);
        }
    }
}