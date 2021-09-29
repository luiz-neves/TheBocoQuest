using UnityEngine;
using System.Collections;

[System.Serializable]
public class ColorToPrefab
{
	public Color32 color;
	public GameObject prefab;
	public string groupName;
}

[System.Serializable]
public class MapOptions
{
	public bool clearMap;
	public bool reloadMap;
	public bool constantMapReload;
	[Header("Block Spacing (use scale value):")]
	public float scale;
}

public class levelLoader : MonoBehaviour
{

	public Texture2D mapImage;
	public ColorToPrefab[] colorToPrefab;
	public MapOptions options;

	void Start ()
	{
		options.scale = 2;
		LoadLevel ();
	}

	void Update ()
	{
		if (options.clearMap == true) {
			print ("clear");
			ClearLevel ();
			options.clearMap = false;
		}
		if (options.reloadMap == true) {
			print ("reload");
			LoadLevel ();
			options.reloadMap = false;
		}
		if (options.constantMapReload == true) {
			LoadLevel ();
		}
	}

	void ClearLevel ()
	{
		while (transform.childCount > 0) {
			Transform c = transform.GetChild (0);
			c.SetParent (null);
			Destroy (c.gameObject);
		}
	}

	void LoadLevel ()
	{
		ClearLevel ();
		GameObject scenario = new GameObject ("Scenario");
		Color32[] mapImagePixels = mapImage.GetPixels32 ();
		int width = mapImage.width;
		int height = mapImage.height;

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				PlaceTile (mapImagePixels [(y * width) + x], x, y, scenario.transform);
			}
		}

	}

	void PlaceTile (Color32 c, int x, int y, Transform scenario)
	{

		if (c.a == 0) {
			return;
		}

		foreach (ColorToPrefab ctp in colorToPrefab) {
			if (c.Equals (ctp.color)) {
				GameObject go = (GameObject)Instantiate (ctp.prefab, new Vector3 (x, y, 0) * options.scale, Quaternion.identity);

				if (!scenario.FindChild (ctp.groupName)) {
					GameObject parent = new GameObject (ctp.groupName);
					parent.transform.SetParent (scenario);
				}
				go.transform.SetParent (scenario.FindChild (ctp.groupName));
				return;
			}
				
		}
	}
}
