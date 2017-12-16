﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrustumCollider : MonoBehaviour {

	[SerializeField]
	SelectionHandler selectionHandler;

	[SerializeField]
	FieldOfViewHandler fieldOfViewHandler;

	private MeshCollider meshCollider;
	private Mesh frustumMesh;

	void Awake()
	{
		frustumMesh = new Mesh ();
		meshCollider = GetComponent<MeshCollider> ();
	}

	void Start()
	{
		initializeFrustumCollider ();
	}

	void OnTriggerEnter(Collider other)
	{
		var worldObject = other.gameObject.GetComponent<WorldObject> ();
		if (worldObject != null && worldObject.Owner.IsHuman) {
			selectionHandler.ObjectsInsideFrustum.Add (worldObject);
		}

		var unit = worldObject as Unit;
		if (unit != null && !unit.Owner.IsHuman) {
			fieldOfViewHandler.Add (unit);
		}
	}

	void OnTriggerExit(Collider other)
	{
		var worldObject = other.gameObject.GetComponent<WorldObject> ();
		if (worldObject != null && worldObject.Owner.IsHuman) {
			selectionHandler.ObjectsInsideFrustum.Remove (worldObject);
		}

		var unit = worldObject as Unit;
		if (unit != null && !unit.Owner.IsHuman) {
			fieldOfViewHandler.Remove (unit);
		}
	}

	private void initializeFrustumCollider()
	{
		Vector3[] vertices = new Vector3[5];
		int[] triangles;

		var camera = Camera.main;

		//var direction = transform.TransformDirection (Vector3.forward);
		//var right = transform.TransformDirection (Vector3.right);
		//var up = transform.TransformDirection (Vector3.up);

		var direction = Vector3.forward;
		var right = Vector3.right;
		var up = Vector3.up;

		var x = Mathf.Tan (camera.fieldOfView * Mathf.Deg2Rad) * camera.farClipPlane;
		var y = x / camera.aspect;



		vertices [0] = Vector3.zero;
		vertices [1] = direction * camera.farClipPlane + right * x / 2 + up * y / 2;
		vertices [2] = direction * camera.farClipPlane + right * x / 2 - up * y / 2;
		vertices [3] = direction * camera.farClipPlane - right * x / 2 - up * y / 2;
		vertices [4] = direction * camera.farClipPlane - right * x / 2 + up * y / 2;

		triangles = new int[] { 0, 1, 2, 0, 2, 3, 0, 3, 4, 0, 4, 1, 1, 2, 3, 1, 3, 4 };

		frustumMesh.vertices = vertices;
		frustumMesh.triangles = triangles;

		frustumMesh.RecalculateNormals ();

		meshCollider.sharedMesh = frustumMesh;
	}

}
