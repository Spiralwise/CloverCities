using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building {

	Rect area; // Relative to district area
	float height;
	GameObject gameObject;

	public Building (Vector3 dimension, GameObject gameObject) {
		this.area = new Rect(0f, 0f, dimension.x, dimension.z);
		this.height = dimension.y;
		this.gameObject = gameObject;
	}

	public void AttachToDistrict(Transform districtTransform) {
		gameObject.transform.SetParent (districtTransform);
	}

	public Vector3 dimension {
		get {
			return new Vector3 (area.width, height, area.height);
		}
	}

	public Vector2 localPosition {
		get {
			return new Vector2 (area.x, area.y);
		}

		set {
			area.x = value.x;
			area.y = value.y;
			gameObject.transform.localPosition = new Vector3 (area.x, 0f, area.y);
		}
	}
}
