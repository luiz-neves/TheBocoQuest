using UnityEngine;
using System.Collections;

public class cameraBossStage1 : MonoBehaviour {

    public GameObject player;
	public GameObject d;
	public GameObject dBox;
	private importText ds;
	public GameObject boss;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
 		ds = d.GetComponent<importText> ();
		ds.enableDialogBox ();
		boss = GameObject.FindGameObjectWithTag ("Boss1");
		boss.GetComponent<bossScript1> ().inDialog = true;
    }

	// Update is called once per frame
	void Update () {
		if(!dBox.activeSelf){
			boss.GetComponent<bossScript1> ().inDialog = false;
			LockCameraPlayer ();
		}

    }

	void LockCameraPlayer(){
		float x = Mathf.Clamp (player.transform.position.x, -4.3f, 7.7f);
		Vector3 newPosition = new Vector3(x, player.transform.position.y + 2, transform.position.z);
		transform.position = Vector3.Lerp(transform.position, newPosition, Time.time);
	}
}
