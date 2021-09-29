using UnityEngine;
using System.Collections;

public class transition : MonoBehaviour
{
	public float timer;
	float count = 0;
	SpriteRenderer sr;
	float alpha = 0f;
	public float speed;

	void Start ()
	{
		sr = GetComponent<SpriteRenderer> ();
		sr.color = new Color(1f, 1f, 1f, 0f);
	}

	void Update ()
	{
		transform.Translate (new Vector3 (1, 0) * (speed/100));
		if (sr.color.a < 1 && count <= timer) {
			alpha += 0.01f;
			sr.color = new Color (1f, 1f, 1f, alpha);
		} else {
			if (count >= timer) {
				if (sr.color.a > 0) {
					alpha -= 0.004f;
					sr.color = new Color (1f, 1f, 1f, alpha);
				} else {
					Destroy (gameObject);
				}
			} else {
				count += Time.deltaTime;
			}

		}
	}
}
