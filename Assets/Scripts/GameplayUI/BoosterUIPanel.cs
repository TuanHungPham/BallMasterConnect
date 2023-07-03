using UnityEngine;
using TigerForge;
using System.Collections.Generic;

public class BoosterUIPanel : MonoBehaviour
{
    #region public
    #endregion

    #region private
    [SerializeField] private List<BoosterUIButton> itemButtonList = new List<BoosterUIButton>();
    #endregion

    private void Awake()
    {
        LoadComponents();
        ListenEvent();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {

    }

    private void Start()
    {
        InitializeItemButtonList();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.BOOSTER_INVENTORY_CHANGE.ToString(), SetInventoryDataUI);
        EventManager.StartListening(EventID.BOOSTER_USING.ToString(), DeselectAllBooster);
    }

    private void InitializeItemButtonList()
    {
        BoosterUIButton boosterUIButton = null;
        foreach (Transform button in transform)
        {
            boosterUIButton = button.GetComponent<BoosterUIButton>();
            if (itemButtonList.Contains(boosterUIButton)) continue;

            itemButtonList.Add(boosterUIButton);
            boosterUIButton.OnItemClicked += SelectBooster;
        }
    }

    private void SelectBooster(BoosterUIButton booster)
    {
        if (booster.IsSelected())
        {
            booster.Deselect();
            EventManager.EmitEvent(EventID.BOOSTER_DESELECTING.ToString());
            return;
        }

        DeselectAllBooster();

        if (booster.IsEmpty())
        {
            EventManager.EmitEvent(EventID.BOOSTER_DESELECTING.ToString());
            return;
        }

        booster.Select();

        BoosterID selectedBoosterID = booster.GetButtonBoosterID();

        Debug.Log(selectedBoosterID);

        EventManager.SetData(EventID.BOOSTER_SELECTING.ToString(), booster.GetButtonBoosterID());
        EventManager.EmitEvent(EventID.BOOSTER_SELECTING.ToString());
    }

    private void DeselectAllBooster()
    {
        foreach (var button in itemButtonList)
        {
            button.Deselect();
        }
    }

    private void SetInventoryDataUI()
    {
        Debug.Log("Setting Inventory Data UI...");
        List<Booster> inventoryData = (List<Booster>)EventManager.GetData(EventID.BOOSTER_INVENTORY_CHANGE.ToString());

        foreach (var booster in inventoryData)
        {
            if (booster.GetBoosterData() == null) continue;
            SetUIButtonData(booster);
        }
    }

    public void SetUIButtonData(Booster booster)
    {
        Debug.Log("Setting UI Button Data...");
        foreach (var button in itemButtonList)
        {
            if (button.GetButtonBoosterID() != booster.GetBoosterData().BoosterID) continue;

            button.SetDataUI(booster.GetBoosterQuantity());
            return;
        }
    }

    private void EmitSelectingBoosterEvent()
    {
        EventManager.EmitEvent(EventID.BOOSTER_SELECTING.ToString());
    }
}
