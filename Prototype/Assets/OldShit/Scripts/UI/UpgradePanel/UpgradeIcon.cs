using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeIcon : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void UpgradeEvents();
    public static event UpgradeEvents TurnOffUpgradeMode;

    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject unitsCounterPanel;
    [SerializeField] private Text counterText;
    [SerializeField] private Text costText;

    private UpgradePanel upgradePanel;

    private int count;

    public bool Active { get; private set; }
    public Image IconImage { get { return iconImage; } }
    public int Cost { get; private set; }
    public Unit Unit { get; set; }

	private void OnEnable()
	{
        UpgradePanel.UpdateMaxCount += UpdateMaxCount;
	}

	private void OnDisable()
	{
        UpgradePanel.UpdateMaxCount -= UpdateMaxCount;
	}

	private void Start()
	{
        upgradePanel = GetComponentInParent<UpgradePanel>();
	}

	public void OnPointerClick(PointerEventData eventData)
    {
        if (Active && TurnOffUpgradeMode != null)
            TurnOffUpgradeMode();
        SetActive(false);
        upgradePanel.Upgrade(Unit, count, Cost);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetActive(false);
    }

    public void IncreaseCounter()
    {
        count++;
        var maxCount = Mathf.Min(Player.HumanPlayer.ResourcesManager.Money / Cost, upgradePanel.CurrentUnitSet.Count);
        if (count > maxCount)
            count = maxCount;
        UpdateTextInfo();
    }

    public void DecreaseCounter()
    {
        count--;
        if (count < 0)
            count = 0;
        UpdateTextInfo();
    }

    public void UpdateMaxCount()
    {
        if(Active && count > upgradePanel.CurrentUnitSet.Count)
        {
            count = upgradePanel.CurrentUnitSet.Count;
        }
    }

    public void SetCost(int value)
    {
        Cost = value;
        UpdateTextInfo();
    }

    private void SetActive(bool value)
    {
        if (value == Active)
            return;
        
        Active = value;

        if(value)
        {
            count = Mathf.Min(Player.HumanPlayer.ResourcesManager.Money / Cost, upgradePanel.CurrentUnitSet.Count);
            AddCounter();
            UpdateTextInfo();
            SubscribeToInputEvents();
        }
        else
        {
            RemoveCounter();
            UpdateTextInfo();
            UnsubscribeFromInputEvents();
        }
    }

    private void AddCounter()
    {
        unitsCounterPanel.SetActive(true);
        var spaceToFree = unitsCounterPanel.GetComponent<RectTransform>().rect.height;
        //GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, GetComponent<RectTransform>().rect.height + spaceToFree);
        iconImage.transform.position = new Vector3(iconImage.transform.position.x, iconImage.transform.position.y + spaceToFree,0);
    }

    private void RemoveCounter()
    {
        unitsCounterPanel.SetActive(false);
        var spaceToFill = unitsCounterPanel.GetComponent<RectTransform>().rect.height;
        //GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, GetComponent<RectTransform>().rect.height - spaceToFill);
        iconImage.transform.position = new Vector3(iconImage.transform.position.x, iconImage.transform.position.y - spaceToFill,0);
    }

    private void UpdateTextInfo()
    {
        if(Active)
        {
            counterText.text = count.ToString();
            costText.text = (Cost * count).ToString();
        }
        else
        {
            costText.text = Cost.ToString();
        }
    }

    private void SubscribeToInputEvents()
    {
        KeyboardInput.DecreaseCount += DecreaseCounter;
        KeyboardInput.IncreaseCount += IncreaseCounter;
    }

    private void UnsubscribeFromInputEvents()
    {
        KeyboardInput.DecreaseCount -= DecreaseCounter;
        KeyboardInput.IncreaseCount -= IncreaseCounter;
    }
}
