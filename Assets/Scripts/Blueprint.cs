using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum BuildingStyle {
	Stack,
	Corner
}

[System.Serializable]
public class Blueprint {

	public Material defaultMaterial;
	public float minBuildingHeight = 1.0f, maxBuildingHeight = 2.0f;
	public float minBuildingWidth = 1.0f, maxBuildingWidth = 2.0f;
	public int maxStack = 3;
	[Range(0f, 1f)]
	public float heightGrowth = 0.8f;
	public BuildingStyle buildingStyle = BuildingStyle.Corner;

}
