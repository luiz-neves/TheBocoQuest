using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class dialogBoss : MonoBehaviour
{

    private GameObject dialogManager;
    private GameObject canvas;

    private bool isActived = true;


    void Start()
    {
        GameObject dialog = GameObject.FindGameObjectWithTag("Dialog");
        dialogManager = getChildGameObject(dialog, "dialogManager");
        dialogManager.SetActive(false);
        canvas = getChildGameObject(dialog, "Canvas");
        canvas.SetActive(false);
    }

    void FixedUpdate()
    {
        
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
        playerDragon player = playerObject.GetComponent<playerDragon>();
        if (isActived)
        {
            canvas.SetActive(true);
            dialogManager.SetActive(true);
            player.canMove = false;
            player.isMoving = false;
            isActived = false;
        }
    }
}