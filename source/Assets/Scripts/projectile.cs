using UnityEngine;
using System.Collections;

public class projectile : MonoBehaviour {

	public float speed = 10;
	private player playerScript;

	void FixedUpdate () {
		Movement ();
	}

	void Movement(){
		this.transform.Translate (new Vector3 (1, 0, 0) * speed / 100, Space.Self);
	}


	void OnTriggerEnter2D (Collider2D colisor){
		switch (colisor.gameObject.tag) {
		case "Player":
			colisor.gameObject.GetComponent<player> ().LostLife (this.gameObject);
			Destroy (this.gameObject);
			break;
		case "Wall":
			Destroy (this.gameObject);
			break;
		}
	}
}
