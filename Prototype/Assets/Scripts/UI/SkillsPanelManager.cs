using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsPanelManager : MonoBehaviour {

    private const int PerkButtonsOffset = 1;

    [SerializeField] private int perkButtonsCount;
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private SelectionHandler selectionHandler;


    private int lastActiveIndex = PerkButtonsOffset - 1;

	private void Start()
	{
        PlaceButtons();
	}

    public void AddPerk(PerkInfo perkInfo)
    {
        int index = lastActiveIndex + PerkButtonsOffset;
        transform.GetChild(index).GetComponent<Image>().sprite = perkInfo.Image;
        transform.GetChild(index).GetComponent<Button>().interactable = true;
        transform.GetChild(index).GetComponent<Button>().onClick.AddListener(() => 
        {
            selectionHandler.Perks.ActivatePerk(index - PerkButtonsOffset);
            Debug.Log("Activate Perk " + index.ToString());
        });
        lastActiveIndex++;
    }

    public void RemovePerk(PerkInfo perkInfo, int index)
    {
        transform.GetChild(index + PerkButtonsOffset).GetComponent<Button>().onClick.RemoveAllListeners();
        transform.GetChild(index + PerkButtonsOffset).GetComponent<Button>().interactable = false;
        Destroy(transform.GetChild(index + PerkButtonsOffset).gameObject);
        var newButton = Instantiate(buttonPrefab, transform);
        newButton.interactable = false;
        lastActiveIndex--;
    }

    private void PlaceButtons()
    {
        var selectAllButton = Instantiate(buttonPrefab, transform);
        selectAllButton.interactable = true;

        for (int i = 0; i < perkButtonsCount;i++)
        {
            var newButton = Instantiate(buttonPrefab, transform);
        }
    }
}
