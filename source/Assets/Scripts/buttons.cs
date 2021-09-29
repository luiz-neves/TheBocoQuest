using UnityEngine;
using UnityEngine.SceneManagement;

public class buttons : MonoBehaviour {
    
    //public GameObject painelPrincipal;
    //painelPrincipal.SetActive(false); -> desativa o painel

    public void ExitButton()
    {
        Application.Quit();
    }

    public void MenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("menu");
    }
    
    public void PlayButton()
    {
        SceneManager.LoadScene("level1");
    }

    public void ContinueButton()
    {
        GameObject playerDataObject = GameObject.FindGameObjectWithTag("PlayerData");
        playerData playerData = playerDataObject.GetComponent<playerData>();
        SceneManager.LoadScene(playerData.currentLevel);
    }

    public void ContinuePauseButton()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player player = playerObject.GetComponent<player>();
        Time.timeScale = 1f;
        player.pause.SetActive(false);
        player.canMove = true;
        player.GetComponent<Rigidbody2D>().isKinematic = false;
    }

}
