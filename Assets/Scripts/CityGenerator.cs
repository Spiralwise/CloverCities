using UnityEngine;
using System.Collections;

public class CityGenerator : MonoBehaviour {

	public int mapWidth, mapHeight;

	BuildingGenerator buildingGenerator;

	void Start() {
		this.buildingGenerator = new BuildingGenerator ();
		generateCity ();
	}

	void Update() {
		
	}

	public void generateCity() {
		buildingGenerator.createBuilding ();
	}
}
