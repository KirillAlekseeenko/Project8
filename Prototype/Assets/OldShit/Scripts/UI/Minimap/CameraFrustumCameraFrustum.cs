using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]
public class CameraFrustum : MonoBehaviour
{
  public Camera camera;
  [SerializeField]
  private float distance;
  private void OnDrawGizmos()
  {
    Vector3 p1 = camera.ScreenToWorldPoint(new Vector3 (0f, 0f, distance));
    Vector3 p2 = camera.ScreenToWorldPoint(new Vector3(0f, camera.pixelHeight, distance));
    Vector3 p3 = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight, distance));
    Vector3 p4 = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, 0f, distance));
    Gizmos.DrawLine(p1, p2);
    Gizmos.DrawLine(p2, p3);
    Gizmos.DrawLine(p3, p4);
    Gizmos.DrawLine(p4, p1);
  }
}
