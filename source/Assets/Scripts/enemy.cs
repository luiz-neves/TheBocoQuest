using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {

    public bool direction;
    public float speed;
    //public Transform groundCheck;
	public Rigidbody2D playerTransform;

    void Start () {
		playerTransform = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D>();
    }

    void Update () {

        if (direction)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D colisor)
    {
        if (colisor.gameObject.tag == "GroundCheck")
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            player playerScript = playerObject.GetComponent<player>();
            playerScript.score++;
            playerScript.PlaySound();
			playerTransform.velocity = new Vector2 (playerTransform.velocity.x, 0);
			playerTransform.AddForce (transform.up * 250);
            GetComponent<AudioSource>().Play();
            gameObject.SetActive(false);


        }
        /*else if (colisor.gameObject.tag == "Player")
        {
            var player = colisor.gameObject.GetComponent<player>();
            player.perderVida();
        }*/
        
    }

    void OnCollisionEnter2D(Collision2D colisor)
    {
        if (colisor.gameObject.tag == "Wall" || colisor.gameObject.tag == "Player" || colisor.gameObject.tag == "Obstacle" || colisor.gameObject.tag == "Poing") { 
            if (direction)
            {
                this.GetComponentInChildren<SpriteRenderer>().flipX = true;
            }else { 
                this.GetComponentInChildren<SpriteRenderer>().flipX = false;
            }
            direction = !direction;
        }
    }
}
