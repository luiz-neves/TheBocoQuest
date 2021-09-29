using UnityEngine;
using System.Collections;

public class mage : MonoBehaviour {

    public bool canFire = false;
    public GameObject fire;
    public float timer;
	void FixedUpdate () {
        if (canFire)
        {
            if (timer <= 0)
            {
                GetComponent<AudioSource>().Play();
                Instantiate(fire, new Vector3(4.71f, -4.884086f, 0), Quaternion.identity);
                timer = 0.52f;
            }
            timer -= 0.02f;
        }
	}

    void LateUpdate()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerDragon player = playerObject.GetComponent<playerDragon>();
        canFire = player.canMove;
    }
}
