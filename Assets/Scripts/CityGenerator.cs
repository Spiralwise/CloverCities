using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DistrictManager))]
public class CityGenerator : MonoBehaviour {

	public int mapWidth = 100, mapHeight = 100;

	void Start() {
		GenerateCity ();
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Space))
			Reset ();
	}

	public void GenerateCity() {
		DistrictManager districtGenerator = GetComponent<DistrictManager> ();
		GameObject district = districtGenerator.CreateDistrict ();
		district.transform.position = Vector3.zero;
		district.transform.SetParent (transform);
	}

	void Reset () {
		foreach (Transform asset in transform)
			Destroy (asset.gameObject);
		GenerateCity ();
	}
}
