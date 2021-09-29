using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class importTextEnd : MonoBehaviour {

    public GameObject dialogBox;

    public Text text;
    public TextAsset fileText;

    public string[] line;

    public int currentLine = 0;
    public int finalLine = 0;
	
    void Start()
    {
        if (fileText != null)
        {
            line = (fileText.text.Split('\n'));
        }
        if (finalLine == 0)
        {
            finalLine = line.Length - 1;
        }
    }
	void Update () {
		if (dialogBox.active) {
			try {
				
				if (Input.anyKeyDown) {
					currentLine += 1;
				}
				if (currentLine > finalLine) {
					disableDialogBox ();
                    SceneManager.LoadScene("theEnd");
                    Destroy(this.gameObject);
                  
                }
                text.text = line[currentLine];
            } catch (System.IndexOutOfRangeException e) {
				print (e);
			}
		}
	}

    public void enableDialogBox()
    {
        dialogBox.SetActive(true);
    }

    public void disableDialogBox()
    {
        dialogBox.SetActive(false);
    }
}
