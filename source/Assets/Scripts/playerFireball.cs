using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class playerFireball : MonoBehaviour {

    private GameObject playerInterface;
    public Slider mageHp;
    public GameObject p;

	void Start () {
        p = GameObject.FindGameObjectWithTag("Player");
        playerInterface = GameObject.Find("Interface").GetComponent<GameObject>();
        GameObject a = GameObject.FindGameObjectWithTag("Respawn");
        playerFireball b = a.GetComponent<playerFireball>();
        mageHp = b.mageHp;
        this.transform.eulerAngles = new Vector3(0, 180, 0);
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * 300);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        if(mageHp.value <= 0)
        {
            p.GetComponent<playerDragon>().currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(p.GetComponent<playerDragon>().currentLevel);
        }
    }

    void OnTriggerEnter2D(Collider2D colisor)
    {
        if (colisor.CompareTag("MageFace"))
        {
            mageHp.value -= 1;
            Destroy(this.gameObject);
        }
        else if (!colisor.CompareTag("Player") && !colisor.CompareTag("Mage"))
        {
            Destroy(this.gameObject);
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
