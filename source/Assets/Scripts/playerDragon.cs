using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class playerDragon : MonoBehaviour
{

	public float speed;
    private float fireTimer;

    public int lifes;
    public int score;
    public int currentLevel = 3;
    private int mageFires;

	public bool canMove;
	public bool isMoving;

	public Transform playerTransform;
	private Transform enemy;
	private SpriteRenderer sr;

	private Rigidbody2D rb;

	private Animator playerAnimator;

    public GameObject pause;
    public GameObject fire;

    void Start ()
	{
        rb = GetComponent<Rigidbody2D> ();
        sr = getChildGameObject(this.gameObject, "player").GetComponent<SpriteRenderer> ();
        rb.gravityScale = 0;
		if (SceneManager.GetActiveScene ().buildIndex != 3) {
			GameObject playerDataObject = GameObject.FindGameObjectWithTag ("PlayerData");
			playerDragonData playerData = playerDataObject.GetComponent<playerDragonData> ();
			lifes = playerData.lifes;
            score = playerData.score;
			currentLevel = playerData.currentLevel;
		}
    }

    void Awake()
    {
        GameObject pauseRoot = GameObject.FindGameObjectWithTag("Pause");
        pause = pauseRoot.transform.Find("Pause").gameObject;
        pause.SetActive(false);
    }

	void Update ()
	{
		var guiText = GameObject.Find ("Life").GetComponent<GUIText> ();
		guiText.text = "Lifes: " + lifes.ToString ();
        guiText = GameObject.Find("Score").GetComponent<GUIText>();
        guiText.text = "Score: " + score.ToString();
    }

    void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 0);
        Pause();
        MoveControl();
        Fire();
    }

    void OnCollisionEnter2D (Collision2D colisor)
	{
	}

    void OnTriggerEnter2D(Collider2D colisor)
    {
        switch (colisor.gameObject.tag)
        {
            case "Coin":
                score++;
                GameObject coin = colisor.gameObject;
                Destroy(colisor);
                Destroy(coin, 0.1f);
                break;
            case "MageFireball":
                mageFires++;
                if (mageFires >= 3)
                {
                    mageFires = 0;
                    lifes--;
                    if (lifes <= 0)
                    {
                        SceneManager.LoadScene("gameOver");
                    }
                }
                break;
        }
    }

	void Move ()
	{
		float axisx = Input.GetAxisRaw ("Horizontal");
		if (Input.GetAxisRaw ("Vertical") > 0) {
				rb.velocity = new Vector2 (rb.velocity.x, 0);
				rb.AddForce (transform.up * speed);
		} else if (Input.GetAxisRaw("Vertical") < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(-transform.up * speed);
        }
		Vector2 movement = new Vector2 (axisx, 0);
		rb.AddForce (movement * speed);

		if (axisx != 0) {
			isMoving = true;
		} else {
			isMoving = false;
		}
	}

    public void Pause()
    {
        if (Input.GetButtonDown("Cancel") && canMove)
        {
            canMove = false;
            Time.timeScale = 0f;
            GetComponent<Rigidbody2D>().isKinematic = true;
            pause.SetActive(true);
        }
    }

    public void MoveControl()
    {
        if (canMove)
        {
            Move();
        }
        Vector2 noMovement = new Vector2(0, 0);
        rb.velocity = noMovement;
    }

    public void Fire()
    {
        fireTimer -= 0.02f;
        if (Input.GetKeyDown(KeyCode.Space) && fireTimer <= 0 && canMove)
        {
            fireTimer = 1;
            Instantiate(fire, new Vector3((this.gameObject.transform.position.x + 1.7f), (this.gameObject.transform.position.y + 0.15f), 0), Quaternion.identity);
        }
    }

    static public GameObject getChildGameObject(GameObject fromGameObject, string withName)
    {
        //Author: Isaac Dart, June-13.
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }
}


//transform.eulerAngles = new Vector2(0, 180);
//transform.Translate(Vector2.right * 200 * Time.deltaTime);

//transform.Translate(Vector2.right * speed * Time.deltaTime);
//GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed * Time.deltaTime);