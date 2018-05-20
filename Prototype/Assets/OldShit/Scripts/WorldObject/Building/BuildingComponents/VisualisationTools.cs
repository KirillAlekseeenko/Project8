using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using NUnit.Framework;

[System.Serializable]
public class BuildingLevelParams{
	public Sprite image;
	public GameObject model;
	public int cost;
}

public class VisualisationTools : MonoBehaviour {

	[Header("Building common parameters")]
	[SerializeField]
	private string buildingName;
	[SerializeField]
	public List<BuildingLevelParams> buildingLevels;
	[SerializeField]
	private GameObject currentModel;
	private List<MeshRenderer> buildingRenderers;
	private Renderer objRenderer;
	private Color mainColor;
	private IEnumerator blink;

	void Awake(){
		blink = Blink ();
		buildingRenderers = new List<MeshRenderer> ();
		setRenderers();

		currentModel.transform.parent = this.gameObject.transform;
	}

	private void setRenderers(){
		buildingRenderers.Clear ();
		foreach(MeshRenderer renderer in gameObject.GetComponentsInChildren<MeshRenderer>()){
			buildingRenderers.Add (renderer);
		}
	}

	private IEnumerator Blink() {
		float maxAlpha = 1f;
		float minAlpha = 0.4f;
		float alphaDelta = 0.05f;
		while(true){
			if (buildingRenderers[0].material.color.a <= minAlpha || buildingRenderers[0].material.color.a >= maxAlpha)
				alphaDelta = -alphaDelta;
			foreach (MeshRenderer obj in buildingRenderers) {
				obj.material.SetColor("_Color", new Color(obj.material.color.r,
					obj.material.color.g,
					obj.material.color.b,
					obj.material.color.a + alphaDelta));
			}
			yield return new WaitForSeconds(0.1f);
		}
	}

	public void SetModel(int level){
		if (currentModel != null) {
			GameObject temp = null;
			temp = (GameObject)Instantiate (buildingLevels [level - 1].model);
			temp.transform.parent = currentModel.transform.parent;
			temp.transform.localPosition = currentModel.transform.localPosition;
			temp.transform.localRotation = currentModel.transform.localRotation;
			temp.transform.localScale = currentModel.transform.localScale;
			temp.transform.SetSiblingIndex (currentModel.transform.GetSiblingIndex ());
			DestroyImmediate (currentModel);
			//currentModel.SetActive (false);
			currentModel = temp;
			currentModel.SetActive (true);
			setRenderers ();
		}
	}

	public void SetBlinking(bool blinking){
		if (blinking)
			StartCoroutine (blink);
		else {
			StopCoroutine (blink);
			foreach (MeshRenderer obj in buildingRenderers) {
				obj.material.SetColor("_Color", new Color(obj.material.color.r,
					obj.material.color.g,
					obj.material.color.b, 1));
			}
		}
	}
}
