using UnityEngine;
using System.Collections;

public class arrowLauncher : MonoBehaviour
{

	private float timer = 0;
	public float refreshTime = 2;
	public bool leftOrRight;
	private int dir = 1;
	private bool iHaveToFire = false;
	public GameObject arrow;

	void Start ()
	{
		//arrow = GameObject.Find ("Arrow");
	}

	void Update ()
	{
		if (leftOrRight) {
			dir = 1;
		} else {
			dir = -1;
		}

		if (iHaveToFire) {
			if(timer >= refreshTime){
                GetComponent<AudioSource>().Play();
                Instantiate (arrow, new Vector3 (transform.position.x + (transform.localScale.x/8.1f * dir), transform.position.y), new Quaternion (0, 0, ((dir == 1) ? 0 : 180), 0));
				timer = 0;
			}
			timer += Time.deltaTime;
		}
	}

	void OnTriggerEnter2D (Collider2D colisor)
	{
		if (colisor.gameObject.tag == "Player") {
			iHaveToFire = true;
		}
	}

	//	void OnTriggerStay2D(Collider2D colisor){
	//		if (colisor.gameObject.tag == "Player") {
	//			timer += Time.deltaTime;
	//			if (timer >= refreshTime) {
	//				print ("Lançar Flecha");
	//				Instantiate(arrow, new Vector3(transform.position.x + (transform.localScale.x * dir), transform.position.y), new Quaternion(0,0,dir,0));
	//				timer = 0;
	//			}
	//		}
	//	}

	void OnTriggerExit2D (Collider2D colisor)
	{
		switch (colisor.gameObject.tag) {
		case "Player":
			iHaveToFire = false;
			timer = 0;
			break;
		case "Arrow":
			Destroy (colisor.gameObject);
			break;
		}
	}
}
