using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class importTextDragon : MonoBehaviour {

    public GameObject dialogBox;

    public Text text;
    public TextAsset fileText;

    public player player;
    public playerDragon playerD;

    public string[] line;

    public int currentLine;
    public int finalLine = 0;

	void Start () {
        playerD = FindObjectOfType<playerDragon>();
        setText(fileText);
	    
	}
	
	void Update () {
		if (dialogBox.active) {
			try {
				
				if (Input.anyKeyDown) {
					currentLine += 1;
				}
				if (currentLine > finalLine) {
					disableDialogBox ();
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
        playerD.canMove = false;
    }

    public void disableDialogBox()
    {
        dialogBox.SetActive(false);
        playerD.canMove = true; 
    }

    public void setText(TextAsset fileT)
    {
        finalLine = 0;
        currentLine = 0;
        if (fileT != null)
        {
            line = (fileT.text.Split('\n'));
        }
        if (finalLine == 0)
        {
            finalLine = line.Length;
        }
    }
}
