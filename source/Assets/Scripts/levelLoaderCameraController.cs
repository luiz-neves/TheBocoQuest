using UnityEngine;
using System.Collections;

[System.Serializable]
public class ControllerOptions
{
	[Header("CONTROLS:")]
	[Header("A S D W: Fast Camera Movement")]
	[Header("Arrow Keys: Slow Camera Movement")]
	[Header("Keypad + : Zoom In")]
	[Header("Keypad - : Zoom Out")]
	[Header("")]
	public int speed = 5;
	public int zoom = 100;
}
public class levelLoaderCameraController : MonoBehaviour {

	public ControllerOptions options;
	private GameObject cam;

	void Start(){
		cam = Camera.main.gameObject;
	}

	void Update () {
		Move ();
		Zoom ();
	}

	void Move(){
		float dirX = 0;
		float dirY = 0;

		if (Input.GetKeyDown (KeyCode.LeftArrow)){
			dirX = -1;
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			dirX = 1;
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			dirY = -1;
		} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			dirY = 1;
		}
		if (Input.GetKey (KeyCode.W)) {
			dirY = 1f/2f;
		}
		if (Input.GetKey (KeyCode.S)) {
			dirY = -1f/2f;
		}
		if (Input.GetKey (KeyCode.A)) {
			dirX = -1f/2f;
		}
		if (Input.GetKey (KeyCode.D)) {
			dirX = 1f/2f;
		}

		Vector3 direction = new Vector3 (dirX,dirY,0);
		cam.transform.position += direction * options.speed;
	}

	void Zoom(){
		Camera camZoom = cam.GetComponent<Camera> ();
		camZoom.orthographicSize = options.zoom / 5;

		if (Input.GetKeyDown (KeyCode.KeypadPlus)) {
			options.zoom -= 10;
		} else if (Input.GetKeyDown (KeyCode.KeypadMinus)) {
			options.zoom += 10;
		};
	}
}
