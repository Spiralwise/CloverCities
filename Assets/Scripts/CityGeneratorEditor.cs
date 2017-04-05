using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (CityGenerator))]
public class CityGeneratorEditor : Editor {

	public override void OnInspectorGUI() {
		CityGenerator cityGen = (CityGenerator)target;

		DrawDefaultInspector ();

		if (GUILayout.Button ("Generate")) {
			cityGen.generateCity ();
		}
	}
}
