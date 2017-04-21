using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DistrictLayer {

	public string name;
	public float margin;
	public Rect area;
	public Blueprint blueprint;

	List<Building> buildings;

	public GameObject CreateDistrict () {
		buildings = new List<Building> ();
		GameObject district = new GameObject (name);

		Architect architect = new Architect ();
		Rect workingArea = new Rect (margin, margin, area.width - margin * 2, area.height - margin * 2);
		Vector2 endArea = new Vector2 (
			                  workingArea.x + workingArea.width - margin - blueprint.minBuildingWidth,
			                  workingArea.y + workingArea.height - margin - blueprint.minBuildingWidth);
		while (workingArea.y < endArea.y) {
			float nextY = workingArea.y;
			while (workingArea.x < endArea.x) {
				Building building = architect.CreateBuilding (blueprint);
				building.AttachToDistrict (district.transform);
				building.localPosition = workingArea.position;
				buildings.Add (building);

				workingArea.xMin += building.dimension.x + margin;
				float estimatedNextY = workingArea.y + building.dimension.z + margin;
				if (estimatedNextY > nextY)
					nextY = estimatedNextY;
			}
			workingArea.xMin = margin;
			workingArea.yMin = nextY;
		}

		return district;
	}

	/*void OnDrawGizmos () {
		Gizmos.color = Color.blue;
		if (buildings != null) {
			foreach (Building building in buildings) {
				Vector3 northWest = new Vector3 (building.localPosition.x, 0, building.localPosition.y + building.dimension.z);
				Vector3 northEast = new Vector3 (building.localPosition.x + building.dimension.x, 0, building.localPosition.y + building.dimension.z);
				Vector3 southEast = new Vector3 (building.localPosition.x + building.dimension.x, 0, building.localPosition.y);
				Vector3 southWest = new Vector3 (building.localPosition.x, 0, building.localPosition.y);
				Gizmos.DrawLine (northWest, northEast);
				Gizmos.DrawLine (northEast, southEast);
				Gizmos.DrawLine (southEast, southWest);
				Gizmos.DrawLine (southWest, northWest);
			}
		}
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube (new Vector3(area.center.x, 0f, area.center.y), new Vector3(area.width, 0f, area.height));
	}*/
}
