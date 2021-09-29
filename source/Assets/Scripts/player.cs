using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class player : MonoBehaviour
{

	public float speed;
	public float jump;
    public float maximumSpeed = 10;

	public int lifes;
    public int score;
    public int currentLevel = 3;
    private int defendingTimer;
    private int timer;
	private int jumpTimer;
    private int jumpPowerupTimer;
    private int deathTimer;

	public bool isOnGround;
	public bool launchingPlayer;
	public bool canMove;
	public bool isMoving;
    public bool isMounted;
    public bool powerupActived;
    public bool isDefending;

	public Transform groundCheck;
	public Transform playerTransformAnimator;
	public Transform playerTransform;
	private Transform enemy;
	SpriteRenderer sr;

	private Rigidbody2D rb;

	private Animator playerAnimator;

    public GameObject pause;

    public Slider staminaBar;

    public AudioClip coinSound;
    public AudioClip poingSound;
    public AudioClip hitSound;
    public AudioClip jumpSound;
    public AudioClip horseSound;
    public AudioClip defendSound;
    public AudioClip killSound;

    void Start ()
	{
        rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		playerAnimator = playerTransformAnimator.GetComponent<Animator> ();
        staminaBar.value = 15;
        deathTimer = 27000;
		if (SceneManager.GetActiveScene ().buildIndex != 3) {
			GameObject playerDataObject = GameObject.FindGameObjectWithTag ("PlayerData");
			playerData playerData = playerDataObject.GetComponent<playerData> ();
			lifes = playerData.lifes;
            score = playerData.score;
			currentLevel = playerData.currentLevel;
		}
        if (SceneManager.GetActiveScene().buildIndex == 7 || SceneManager.GetActiveScene().buildIndex == 8)
        {
            lifes = 4;
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
        playerAnimator.SetBool ("isMoving", isMoving);
        playerAnimator.SetBool ("isMounted", isMounted);
    }

    void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 0);
        Pause();
        Defend();
        HitObstacle();
        MoveControl();
        FallingSpeed();
        jumpPowerupTimer--;
        if (jumpPowerupTimer <= 0)
        {
            powerupActived = false;
        }
    }

    void OnCollisionEnter2D (Collision2D colisor)
	{
		switch (colisor.gameObject.tag) {
		case "Enemy":
			LostLife (colisor.gameObject);
			break;
        case "Obstacle":
            //SceneManager.LoadScene ("gameOver");
            deathTimer = 10;
            canMove = false;
            break;
        }
	}

    void OnTriggerEnter2D(Collider2D colisor)
    {
        switch (colisor.gameObject.tag)
        {
            case "Coin":
                score++;
                GetComponent<AudioSource>().PlayOneShot(coinSound);
                Destroy(colisor.gameObject);
                break;
            case "Poing":
                GetComponent<AudioSource>().PlayOneShot(poingSound);
                rb.velocity = new Vector2(0, 15);
                //rb.AddForce(Vector2.up * 20, ForceMode2D.);
                break;
        }
    }

    void OnTriggerStay2D(Collider2D colisor)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (colisor.gameObject.tag)
            {
                case "Horse":
                    GetComponent<AudioSource>().PlayOneShot(horseSound);
                    isMounted = true;
                    speed += 150;
                    Destroy(colisor.gameObject);
                    break;
            }
        }
    }

	void Move ()
	{
		isOnGround = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
		float axisx = Input.GetAxisRaw ("Horizontal");
		if (axisx > 0) {
			sr.flipX = false;
		}
		if (axisx < 0) {
			sr.flipX = true;
		}
		if(jumpTimer <= 0){
			if (Input.GetAxisRaw ("Vertical") > 0 && isOnGround) {
                GetComponent<AudioSource>().PlayOneShot(jumpSound);
                rb.velocity = new Vector2 (rb.velocity.x, 0);
				rb.AddForce (transform.up * jump);
				jumpTimer = 1;
			}
          
        } else {
			jumpTimer--;
		}
		Vector2 movement = new Vector2 (axisx, 0);
		rb.AddForce (movement * speed);

		if (axisx != 0) {
			isMoving = true;
		} else {
			isMoving = false;
		}
	}

    public void LostLife(GameObject enemy)
    {
        if (!launchingPlayer && !isDefending && !isMounted) {
            lifes -= 1;
            int side = (int)Mathf.Sign(transform.position.x - enemy.transform.position.x);
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2((float)side, 0.5f) * 300);

            if (lifes <= 0) {
                SceneManager.LoadScene("gameOver");
            }
            launchingPlayer = true;
            timer = 50;
            if (isMounted)
            {
                isMounted = false;
                speed = speed - 150;
                lifes++;
            }
            GetComponent<AudioSource>().PlayOneShot(hitSound);
        } else if (isMounted)
        {
            isMounted = false;
        }
        if (isDefending)
        {
            GetComponent<AudioSource>().PlayOneShot(defendSound);
        }
    }

    public void Defend()
    {
        if (canMove && Input.GetKey(KeyCode.LeftShift) && staminaBar.value >= 0 && defendingTimer <= 0 && isOnGround && !isMounted)
        {
            isDefending = true;
            staminaBar.value -= 0.1f;
            if (staminaBar.value <= 0)
            {
                defendingTimer = 150;
            }
        }
        else
        {
            isDefending = false;
            staminaBar.value += 0.1f;
        }
        if (defendingTimer > 0)
        {
            defendingTimer--;
        }
        playerAnimator.SetBool("isDefending", isDefending);
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

    public void HitObstacle()
    {
        if (deathTimer > 0 && deathTimer != 27000)
        {
            deathTimer--;
        }
        else if (deathTimer != 27000)
        {
            lifes--;
            if (lifes > 0)
            {
                GameObject playerDataObject = GameObject.FindGameObjectWithTag("PlayerData");
                playerData playerData = playerDataObject.GetComponent<playerData>();
                playerData.lifes = this.lifes;
                SceneManager.LoadScene(currentLevel);
            }
            else
            {
                SceneManager.LoadScene("gameOver");
            }
        }
    }

    public void MoveControl()
    {
        if (canMove && !launchingPlayer && !isDefending)
        {
            Move();
        }
        if (!launchingPlayer)
        {
            float y = rb.velocity.y;
            Vector2 noMovement = new Vector2(0, y);
            rb.velocity = noMovement;
        }
        else {
            timer--;
            if (timer <= 0)
            {
                launchingPlayer = false;
            }
        }
    }

    public void FallingSpeed()
    {
        float speed2 = Vector3.Magnitude(rb.velocity);
        if (speed2 > maximumSpeed)
        {
            float brakeSpeed = speed2 - maximumSpeed;
            Vector3 normalisedVelocity = rb.velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;
            rb.AddForce(-brakeVelocity);
        }
    }

    public void PlaySound()
    {
        GetComponent<AudioSource>().PlayOneShot(killSound);
    }
}


//transform.eulerAngles = new Vector2(0, 180);
//transform.Translate(Vector2.right * 200 * Time.deltaTime);

//transform.Translate(Vector2.right * speed * Time.deltaTime);
//GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed * Time.deltaTime);