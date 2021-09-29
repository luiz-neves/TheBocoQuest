using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class pause : MonoBehaviour {

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex < 3)
        {
            gameObject.SetActive(false);
        }
    }

}
