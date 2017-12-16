using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionArcComponent : MonoBehaviour {

	private Unit unitComponent;

	private float viewRadius;
	private float viewAngle;

	private LayerMask buildingMask;

	private float meshResolution = 0.7f;
	private int edgeResolveIterations = 4;
	private float edgeDstThreshold = 0.5f;

	[SerializeField]
	private Material viewMeshMaterial;
	[SerializeField]
	private GameObject viewPrefab;


	private MeshFilter viewMeshFilter;
	private Mesh viewMesh;

	private GameObject viewGameObject;


	private bool isTurnedOn;

	// Use this for initialization
	void Start () {

		unitComponent = GetComponent<Unit> ();

		viewRadius = unitComponent.pLOS;
		viewAngle = RTS.Constants.VisionArcAngle;

		buildingMask = LayerMask.GetMask ("Building");

		viewGameObject = Instantiate (viewPrefab, gameObject.transform);
		viewMeshFilter = viewGameObject.AddComponent<MeshFilter> ();
		var meshRenderer = viewGameObject.AddComponent<MeshRenderer> ();
		viewGameObject.layer = LayerMask.NameToLayer ("ViewMesh");
		viewGameObject.transform.Translate (new Vector3 (0,-0.3f,0));
		viewGameObject.name = "viewGameObject";

		meshRenderer.material = viewMeshMaterial;
		meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		meshRenderer.receiveShadows = false;

		viewMesh = new Mesh ();
		viewMeshFilter.mesh = viewMesh;
		
	}
	
	// Update is called once per frame


	void LateUpdate()
	{
		viewGameObject.transform.localPosition = Vector3.zero;
		if(isTurnedOn)
			DrawFieldOfView ();
	}

	public bool IsTurnedOn {
		get {
			return isTurnedOn;
		}
		set {
			if (value) {
				viewGameObject.SetActive (true);
			} else {
				viewGameObject.SetActive (false);
			}
			isTurnedOn = value;
		}
	}


	private void DrawFieldOfView()
	{
		int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
		float stepAngleSize = viewAngle / stepCount;
		List<Vector3> viewPoints = new List<Vector3> ();
		ViewCastInfo oldViewCast = new ViewCastInfo ();
		for (int i = 0; i <= stepCount; i++) {
			float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
			ViewCastInfo newViewCast = ViewCast (angle);

			if (i > 0) {
				bool edgeDstThresholdExceeded = Mathf.Abs (oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;
				if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded)) {
					EdgeInfo edge = FindEdge (oldViewCast, newViewCast);
					if (edge.pointA != Vector3.zero) {
						viewPoints.Add (edge.pointA);
					}
					if (edge.pointB != Vector3.zero) {
						viewPoints.Add (edge.pointB);
					}
				}

			}


			viewPoints.Add (newViewCast.point);
			oldViewCast = newViewCast;
		}

		int vertexCount = viewPoints.Count + 1;
		Vector3[] vertices = new Vector3[vertexCount];
		int[] triangles = new int[(vertexCount-2) * 3];

		vertices [0] = Vector3.zero;
		for (int i = 0; i < vertexCount - 1; i++) {
			vertices [i + 1] = transform.InverseTransformPoint(viewPoints [i]);

			if (i < vertexCount - 2) {
				triangles [i * 3] = 0;
				triangles [i * 3 + 1] = i + 1;
				triangles [i * 3 + 2] = i + 2;
			}
		}

		viewMesh.Clear ();

		viewMesh.vertices = vertices;
		viewMesh.triangles = triangles;
		viewMesh.RecalculateNormals ();
	}


	EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast) {
		float minAngle = minViewCast.angle;
		float maxAngle = maxViewCast.angle;
		Vector3 minPoint = Vector3.zero;
		Vector3 maxPoint = Vector3.zero;

		for (int i = 0; i < edgeResolveIterations; i++) {
			float angle = (minAngle + maxAngle) / 2;
			ViewCastInfo newViewCast = ViewCast (angle);

			bool edgeDstThresholdExceeded = Mathf.Abs (minViewCast.dst - newViewCast.dst) > edgeDstThreshold;
			if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded) {
				minAngle = angle;
				minPoint = newViewCast.point;
			} else {
				maxAngle = angle;
				maxPoint = newViewCast.point;
			}
		}

		return new EdgeInfo (minPoint, maxPoint);
	}


	ViewCastInfo ViewCast(float globalAngle) {
		Vector3 dir = DirFromAngle (globalAngle, true);
		RaycastHit hit;

		if (Physics.Raycast (transform.position, dir, out hit, viewRadius, buildingMask)) {
			return new ViewCastInfo (true, hit.point, hit.distance, globalAngle);
		} else {
			return new ViewCastInfo (false, transform.position + dir * viewRadius, viewRadius, globalAngle);
		}
	}

	private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}






	private struct ViewCastInfo {
		public bool hit;
		public Vector3 point;
		public float dst;
		public float angle;

		public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle) {
			hit = _hit;
			point = _point;
			dst = _dst;
			angle = _angle;
		}
	}

	private struct EdgeInfo {
		public Vector3 pointA;
		public Vector3 pointB;

		public EdgeInfo(Vector3 _pointA, Vector3 _pointB) {
			pointA = _pointA;
			pointB = _pointB;
		}
	}
}
