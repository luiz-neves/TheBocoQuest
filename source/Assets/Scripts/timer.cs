using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class timer : MonoBehaviour {

    public GameObject timero;
    public GameObject npc;
    private float time = 999;
    private float sec;
    GameObject playerObject;
    player player;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<player>();
    }
    void FixedUpdate()
    {
        
        if (time >= 0)
        {
            print("aff");
            time -= 0.02f;
            var guiText = timero.GetComponent<GUIText>();
            if (time < 60)
            {
                guiText.text = ((int)time).ToString() + " seconds";
            }
            else if (time % 60 >= 10)
            {
                guiText.text = ((time - (time % 60)) / 60).ToString() + ":" + ((int)(time % 60)).ToString();
            }
            else
            {
                guiText.text = ((time - (time % 60)) / 60).ToString() + ":0" + ((int)(time % 60)).ToString();
            }
        }
        if (time <= 0)
        {
            print("vsf");
            timero.SetActive(false);
            SceneManager.LoadScene("level3");
        }
    }

    void OnTriggerEnter2D(Collider2D colisor)
    {
        print("Oi");
        timero.SetActive(true);
        time = 105;
    }
}
