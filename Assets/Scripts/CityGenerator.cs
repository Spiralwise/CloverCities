using UnityEngine;
using System.Collections;

public class CityGenerator : MonoBehaviour {

	public int cityRadius = 100;
	public DistrictLayer center;
	public DistrictLayer[] districtLayers;

	void Start() {
		GenerateCity (); // TODO Maybe in awake?
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Space))
			Reset ();
	}

	public void GenerateCity() {
		GameObject c = center.CreateDistrict ();
		c.transform.SetParent (transform);

		float minimalDistance = Mathf.Sqrt (center.area.width * center.area.width + center.area.height * center.area.height) / 2;
		foreach (DistrictLayer district in districtLayers) {
			float radius = Mathf.Sqrt (minimalDistance * minimalDistance + district.area.height * district.area.height / 4);
			float angleProgression = Mathf.Atan2 ((district.area.width / 2), radius) * Mathf.Rad2Deg;
			for (float currentAngle = 0f; currentAngle < 360f; currentAngle += angleProgression) {
				GameObject d = district.CreateDistrict ();
				d.transform.SetParent (transform);
				d.transform.localPosition = new Vector3(minimalDistance + district.area.y / 2f, 0f, 0f);
				d.transform.RotateAround (transform.position, Vector3.up, currentAngle);
			}
			minimalDistance += radius;
		}
	}

	void Reset () {
		foreach (Transform asset in transform)
			Destroy (asset.gameObject);
		GenerateCity ();
	}
}
