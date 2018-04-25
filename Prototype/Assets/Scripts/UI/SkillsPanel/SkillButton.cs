using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private SkillsPanelManager skillsPanel;
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
        Debug.Log(skillDescription);
        // дальше выводить в подсказки skillDescription
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!button.interactable)
            return;
    }
}
