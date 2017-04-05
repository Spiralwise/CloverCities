using UnityEngine;
using System.Collections;

public class BuildingGenerator : MonoBehaviour {

	public GameObject createBuilding() {
		GameObject building = GameObject.CreatePrimitive (PrimitiveType.Cube);
		return building;
	}
}
