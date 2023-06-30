using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class BoosterInventory : MonoBehaviour
{
    #region public
    #endregion

    #region private
    [SerializeField] private List<Booster> boosterInventoryList = new List<Booster>();
    #endregion

    private void Awake()
    {
        LoadComponents();
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
        Invoke(nameof(EmitEventInventroyChange), 0);
    }

    private void EmitEventInventroyChange()
    {
        EventManager.SetData(EventID.BOOSTER_INVENTORY_CHANGE.ToString(), boosterInventoryList);
        EventManager.EmitEvent(EventID.BOOSTER_INVENTORY_CHANGE.ToString());
    }

    public void AddBoosterToInventory(Booster booster, int quantity)
    {
        if (boosterInventoryList.Contains(booster))
        {
            booster.AddMoreBooster(quantity);
            return;
        }

        booster.AddMoreBooster(quantity);
        boosterInventoryList.Add(booster);
    }

    public Booster GetBoosterInInventory(BoosterID boosterID)
    {
        foreach (Booster booster in boosterInventoryList)
        {
            if (booster.GetBoosterData().BoosterID != boosterID) continue;

            return booster;
        }

        return null;
    }
}
