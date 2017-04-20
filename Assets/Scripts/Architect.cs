﻿using UnityEngine;
using System.Collections;

public class Architect {

	const int faces = 5; // Do not do bottom face
	const int ring = 8;
	const int top = 16;
	const int verticesByStack = faces * 4;

	float heightGrowth;
	BuildingStyle buildingStyle;

	enum Corner {
		NorthWest,
		NorthEast,
		SouthEast,
		SouthWest
	}

	public float RandomVariation {
		get {
			return Mathf.Pow (Random.Range (0f, 1f), 3);
		}
	}

	public Building CreateBuilding(Blueprint blueprint) {
		GameObject building = new GameObject ("Building"); // TODO Give name that can even be displayed in game
		Mesh mesh = building.AddComponent<MeshFilter> ().mesh;
		MeshRenderer renderer = building.AddComponent<MeshRenderer> ();

		this.heightGrowth = blueprint.heightGrowth;
		this.buildingStyle = blueprint.buildingStyle;
		Vector3 dimension = new Vector3 (
			                    Random.Range (blueprint.minBuildingWidth, blueprint.maxBuildingWidth),
			                    Random.Range (blueprint.minBuildingHeight, blueprint.maxBuildingHeight),
								Random.Range (blueprint.minBuildingWidth, blueprint.maxBuildingWidth));
		CreateMesh (ref mesh, dimension, Random.Range (1, blueprint.maxStack + 1));
		renderer.material = blueprint.defaultMaterial;

		return new Building (dimension, building);
	}

	int SetQuad(int [] triangles, int i, int v00, int v10, int v01, int v11) {
		triangles [i] = v00;
		triangles [i + 1] = triangles [i + 4] = v01;
		triangles [i + 2] = triangles [i + 3] = v10;
		triangles [i + 5] = v11;
		return i + 6;
	}

	void CreateMesh(ref Mesh mesh, Vector3 size, int stack) {
		Vector3[] vertices = new Vector3[verticesByStack * stack];
		int[] triangles = new int[faces * 6 * stack];

		switch (buildingStyle) {
		case BuildingStyle.Corner:
			CreateAsCorner (vertices, triangles, size, stack);
			break;
		case BuildingStyle.Stack:
			CreateAsOnTop (vertices, triangles, size, stack);
			break;
		}

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.RecalculateNormals ();
	}

	/* Create a building mesh that stacks cube on each other.
	 */
	void CreateAsOnTop(Vector3[] vertices, int[] triangles, Vector3 size, int stack) {
		float currentHeight = 0f;
		float nextHeight = size.y; // TODO Not good, how can we now the final size?
		Rect surface = new Rect(-size.x/2, -size.z/2, size.x, size.z);

		int v = 0;
		for (int s = 0; s < stack; s++) {
			int currentV;
			int offsetV = s * verticesByStack;
			for (int t = 0; t < 2; t++) {
				int onTop = t % 2;
				currentV = ring * t + offsetV;
				vertices [currentV] = vertices [currentV + 7] = new Vector3 (surface.x, onTop * nextHeight + currentHeight, surface.y);
				vertices [currentV + 1] = vertices [currentV + 2] = new Vector3 (surface.xMax, onTop * nextHeight + currentHeight, surface.y);
				vertices [currentV + 3] = vertices [currentV + 4] = new Vector3 (surface.xMax, onTop * nextHeight + currentHeight, surface.yMax);
				vertices [currentV + 5] = vertices [currentV + 6] = new Vector3 (surface.x, onTop * nextHeight + currentHeight, surface.yMax);
			}
			for (int i = 0; i < 4; i++)
				vertices [top + i + offsetV] = vertices [ring + i * 2 + offsetV];

			int x;
			for (x = offsetV; x < 6 + offsetV; x+=2)
				v = SetQuad (triangles, v, x, x + 1, x + ring, x + ring + 1);
			x = 6 + offsetV;
			v = SetQuad (triangles, v, x, x + 1, x + 8, x + 9);
			x = top + offsetV;
			v = SetQuad (triangles, v, x, x + 1, x + 3, x + 2);

			if (s < stack-1) { // TODO Put it at the beginning
				currentHeight += nextHeight;
				nextHeight *= Random.Range (heightGrowth, 1f);
				surface.xMin += RandomVariation;
				surface.xMax -= RandomVariation;
				surface.yMin += RandomVariation;
				surface.yMax -= RandomVariation;
			}
		}
	}

	/* Create a building mesh that place stacks on the corners.
	 */
	void CreateAsCorner(Vector3[] vertices, int[] triangles, Vector3 size, int stack) {
		Corner corner = (Corner)Random.Range (0, 4);

		int v = 0;
		for (int s = 0; s < stack; s++) {
			Rect surface = new Rect();
			Vector3 dimension = new Vector3 (
				                    size.x * (1f + RandomVariation) / 2, 
									Mathf.Pow ((float)(s + 1) / (stack + 1), heightGrowth) * size.y,
				                    size.z * (1f + RandomVariation) / 2);
			if (corner == Corner.NorthWest)
				surface = new Rect (0f, size.z - dimension.z, dimension.x, dimension.z);
			else if (corner == Corner.NorthEast)
				surface = new Rect (size.x - dimension.x, size.z - dimension.z, dimension.x, dimension.z);
			else if (corner == Corner.SouthEast)
				surface = new Rect (size.x - dimension.x, 0f, dimension.x, dimension.z);
			else if (corner == Corner.SouthWest)
				surface = new Rect (0f, 0f, dimension.x, dimension.z);

			int currentV; // TODO Redundancy with CreateAsOnTop(...), make a function
			int offsetV = s * verticesByStack;
			for (int t = 0; t < 2; t++) {
				int onTop = t % 2;
				currentV = ring * t + offsetV;
				vertices [currentV] = vertices [currentV + 7] = new Vector3 (surface.x, onTop * dimension.y, surface.y);
				vertices [currentV + 1] = vertices [currentV + 2] = new Vector3 (surface.xMax, onTop * dimension.y, surface.y);
				vertices [currentV + 3] = vertices [currentV + 4] = new Vector3 (surface.xMax, onTop * dimension.y, surface.yMax);
				vertices [currentV + 5] = vertices [currentV + 6] = new Vector3 (surface.x, onTop * dimension.y, surface.yMax);
			}
			for (int i = 0; i < 4; i++)
				vertices [top + i + offsetV] = vertices [ring + i * 2 + offsetV];

			int x;
			for (x = offsetV; x < 6 + offsetV; x+=2)
				v = SetQuad (triangles, v, x, x + 1, x + ring, x + ring + 1);
			x = 6 + offsetV;
			v = SetQuad (triangles, v, x, x + 1, x + 8, x + 9);
			x = top + offsetV;
			v = SetQuad (triangles, v, x, x + 1, x + 3, x + 2);

			corner = (corner == (Corner)3) ? 0 : corner + 1;
		}
	}
}