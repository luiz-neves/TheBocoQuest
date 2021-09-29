using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class levelPass : MonoBehaviour {

    private GameObject playerObject;
	public int timer = 0;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerStay2D(Collider2D colider)
    {
		if (timer >= 100) {
			if (colider.gameObject.tag == "Player" || colider.gameObject.tag == "GroundCheck") {
					player player = playerObject.GetComponent<player> ();
					player.currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
					SceneManager.LoadScene ("levelTransition");
			}
		} else {
			timer++;
		}
    }
}
