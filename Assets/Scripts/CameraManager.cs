using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour {

	public Transform target;
	public float sensivity;
	public float scrollSensivity;

	void Start () {
		transform.LookAt (target);
	}
	
	void Update () {
		if (Input.GetMouseButton(0)) {
			transform.RotateAround (target.position, Vector3.up, Input.GetAxis ("Mouse X") * sensivity * 2 * Time.deltaTime);
			transform.Translate (transform.worldToLocalMatrix * transform.up * -1f * Input.GetAxis ("Mouse Y") * sensivity * Time.deltaTime);
			transform.LookAt (target);
		}
		if (Input.GetMouseButton(1)) {
			Vector3 speed = Vector3.up * Input.GetAxis ("Mouse Y") * sensivity / 2 * Time.deltaTime;
			transform.position += speed;
			target.transform.position += speed;
			transform.LookAt (target);
		}
		transform.Translate (transform.worldToLocalMatrix * transform.forward * Input.GetAxis("Mouse ScrollWheel") * scrollSensivity * 10 * Time.deltaTime);
	}
}
