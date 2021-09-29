using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class playerData : MonoBehaviour {

    public int currentLevel = 3;
    public int lifes;
    public int score;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

	void Update () {
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(this.gameObject);
        } else if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            player player = playerObject.GetComponent<player>();
			currentLevel = player.currentLevel;
            lifes = player.lifes;
            score = player.score;
        }
    }

}
