using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BuildingGenerator))]
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
		BuildingGenerator buildingGenerator = GetComponent<BuildingGenerator> ();
		GameObject building = buildingGenerator.CreateBuilding ();
		building.transform.position = Vector3.zero;
		building.transform.SetParent (transform);
	}

	void Reset () {
		foreach (Transform asset in transform)
			Destroy (asset.gameObject);
		GenerateCity ();
	}
}
