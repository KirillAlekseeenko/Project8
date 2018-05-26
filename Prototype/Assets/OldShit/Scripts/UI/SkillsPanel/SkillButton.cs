using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private SkillsPanelManager skillsPanel;
    private Text SkillInfoPanel;
    private Button button;

    private void Start()
    {
        skillsPanel = GetComponentInParent<SkillsPanelManager>();
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!button.interactable)
            return;
        var skillDescription = skillsPanel.GetPerkDescription(transform.GetSiblingIndex());
       
        skillsPanel.PerkInfoPanel.text = skillDescription;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        skillsPanel.PerkInfoPanel.text = "";
        skillsPanel.PerkInfoPanel.transform.parent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 15f);
        if (!button.interactable)
            return;
    }
}
