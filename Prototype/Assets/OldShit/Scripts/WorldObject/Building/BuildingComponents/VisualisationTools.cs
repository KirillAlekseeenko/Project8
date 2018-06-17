using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class BuildingLevelParams{
	public Sprite image;
	public string name;
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
			if(renderer.material.HasProperty("_Color"))
				buildingRenderers.Add (renderer);
		}
	}

	private void setTransparentMode(Material mat){
		mat.SetFloat ("_Mode", 3f);
		mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
		mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
		mat.SetInt("_ZWrite", 0);
		mat.DisableKeyword("_ALPHATEST_ON");
		mat.EnableKeyword("_ALPHABLEND_ON");
		mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
		mat.renderQueue = 3000;
	}

	private void setOpaqueMode(Material mat){
		mat.SetFloat ("_Mode", 0f);
		mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
		mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
		mat.SetInt("_ZWrite", 1);
		mat.EnableKeyword("_ALPHATEST_ON");
		mat.DisableKeyword("_ALPHABLEND_ON");
		mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
		mat.renderQueue = 3000;
	}

	private void resetAlphaChannel(Material mat){
		mat.SetColor ("_Color", new Color (
			mat.color.r,
			mat.color.g,
			mat.color.b, 1));
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
			currentModel = temp;
			currentModel.SetActive (true);
			setRenderers ();
		}
	}

	public void SetBlinking(bool blinking){
		if (blinking) {
			foreach (MeshRenderer obj in buildingRenderers) {
				foreach(Material mat in obj.materials)
					setTransparentMode (mat);
			}
			StartCoroutine (blink);
		}
			else {
			StopCoroutine (blink);
			foreach (MeshRenderer obj in buildingRenderers) {
				foreach (Material mat in obj.materials) {
					setOpaqueMode (mat);
					resetAlphaChannel (mat);
				}
			}
		}
	}

	public string BuildingName{
		get{ return buildingName;}
	}
}
