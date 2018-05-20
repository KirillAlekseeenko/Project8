using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverviewCamera : MonoBehaviour {

    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float radius;

	void Start () {
        
        StartCoroutine(LoopAction());
        
	}

	private IEnumerator LoopAction()
    {
        float currentDistance;
        while(!Mathf.Approximately(currentDistance = Utils.Distance(target.position, transform.position, y: false), radius))
        {
            var vectorToTarget = (target.position - transform.position).normalized;
            var translationVector = new Vector3(vectorToTarget.x, 0, vectorToTarget.z) * Mathf.Sign(currentDistance - radius) * Mathf.Min(Mathf.Abs(radius - currentDistance), speed * Time.deltaTime);
            transform.Translate(translationVector, Space.World);
            transform.LookAt(target);
            //Debug.Log("Prepos");
            yield return null;
        }

        while(true)
        {
            var vectorToTarget = (target.position - transform.position).normalized;
            var angle = speed * Time.deltaTime / radius; // radians
            var tangentVector = Vector3.Cross(Vector3.up, vectorToTarget).normalized;
            var translationVector = (new Vector3(vectorToTarget.x, 0, vectorToTarget.z) *  (1 - Mathf.Cos(angle)) + tangentVector * Mathf.Sin(angle)) * radius;
            transform.Translate(translationVector, Space.World);
            transform.LookAt(target);
            yield return null;
        }
    }
}
