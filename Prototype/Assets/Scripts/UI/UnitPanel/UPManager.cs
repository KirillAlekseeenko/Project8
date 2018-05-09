using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UPManager : MonoBehaviour
{
    [SerializeField] private UnitIcon unitIconPref;
    public GameObject unitInfoPanel;
	public Image unitInfoImage;
    public Text HPOut;
    public Text MAOut;
    public Text RAOut;
    public Text RADOunt;
    public Text SPOut;
    [SerializeField] private UpgradePanel upgradePanel;

    Dictionary<int, UnitIcon> icons;

    public UpgradePanel UpgradePanel { get { return upgradePanel; } }

	private void Awake()
	{
        icons = new Dictionary<int, UnitIcon>();
	}

	void Start()
    {
        unitIconPref.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, gameObject.GetComponent<RectTransform>().rect.height);
    }

    void OnEnable()
    {
        SelectionHandler.OnUnitSelected += AddIcon;
        SelectionHandler.OnUnitUnselected += DeleteIcon;

		BuildingPanelManager.OnUnitAdded += AddIcon;
		BuildingPanelManager.OnUnitRemoved += DeleteIcon;
    }

    void OnDisable()
    {
        SelectionHandler.OnUnitSelected -= AddIcon;
        SelectionHandler.OnUnitUnselected -= DeleteIcon;

		BuildingPanelManager.OnUnitAdded -= AddIcon;
		BuildingPanelManager.OnUnitRemoved -= AddIcon;
    }

    public void DeleteIcon()
    {
        foreach (Transform child in gameObject.transform)
            Destroy(child.gameObject);
    }

    public void DeleteIcon(Unit unit)
    {
        var icon = icons[unit.UnitClassID];
        icon.RemoveUnit(unit);
        if (icon.Count == 0)
        {
            icons.Remove(icon.ClassID);
            unitInfoPanel.SetActive(false);
            Destroy(icon.gameObject);
        }
    }

    public void AddIcon(Unit unit)
    {
        if(!icons.ContainsKey(unit.UnitClassID))
        {
            var newIcon = CreateNewIcon(unit);
            icons.Add(unit.UnitClassID, newIcon);
        }

        icons[unit.UnitClassID].AddUnit(unit);
    }

    private UnitIcon CreateNewIcon(Unit unit)
    {
        var newIcon = Instantiate(unitIconPref, gameObject.transform);
        newIcon.name = (unit.UnitClassID).ToString();
        newIcon.IconImage.sprite = unit.Icon;
        newIcon.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        newIcon.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
        return newIcon;
    }

}
