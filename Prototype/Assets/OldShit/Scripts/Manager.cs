using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	private static Manager instance;

	public static Manager Instance {
		get {
			return instance;
		}
	}
		
	[SerializeField] private SelectionHandler selection;
	[SerializeField] private ActionHandler action;
	[SerializeField] private FieldOfViewHandler fieldOfView;
    [SerializeField] private FogProjector fogOfWar;

	public SelectionHandler selectionHandler { get { return selection; } }
	public ActionHandler actionHandler { get { return action; } }
	public FieldOfViewHandler fieldOfViewHandler { get { return fieldOfView; } }
	public FogProjector fogOfWarHanlder { get { return fogOfWar; } }

	void Awake()
	{
		if (instance == null) { 
			instance = this;
		} else if(instance == this){
			Destroy(gameObject);
		}
	}
}
