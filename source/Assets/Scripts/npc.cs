using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class npc : MonoBehaviour {

    public GameObject timer;
    public GameObject dialogManager;
    public GameObject canvas;
    public TextAsset fileText;

    private bool isActived = true;

    public float time = 2270000;

    void Start () {
        GameObject dialog = GameObject.FindGameObjectWithTag("Dialog");
        //dialogManager = getChildGameObject(dialog, "dialogManager");
        //dialogManager.SetActive(false);
        //canvas = getChildGameObject(dialog, "Canvas");
        canvas.SetActive(false);
    }
	
    static public GameObject getChildGameObject(GameObject fromGameObject, string withName)
    {
        //Author: Isaac Dart, June-13.
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }

    void OnTriggerEnter2D(Collider2D colisor)
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player player = playerObject.GetComponent<player>();
        if (isActived && colisor.gameObject.tag == "Player")
        {
            canvas.SetActive(true);
            dialogManager.GetComponent<importText>().setText(fileText);
            dialogManager.GetComponent<importText>().enableDialogBox();
            player.isMoving = false;
            isActived = false;
      
        }
    }
}
