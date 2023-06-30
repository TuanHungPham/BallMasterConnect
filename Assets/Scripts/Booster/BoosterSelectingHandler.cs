using UnityEngine;
using TigerForge;

public class BoosterSelectingHandler : MonoBehaviour
{
    #region public
    #endregion

    #region private
    [SerializeField] private Booster currentSelectedBooster;
    [SerializeField] private BoosterID currentSelectedBoosterID;
    [SerializeField] private bool isUsingItem;
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

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.BOOSTER_SELECTING.ToString(), SelectUsingBooster);
        EventManager.StartListening(EventID.BOOSTER_DESELECTING.ToString(), UnuseBooster);
    }

    private void SelectUsingBooster()
    {
        currentSelectedBoosterID = (BoosterID)EventManager.GetData(EventID.BOOSTER_SELECTING.ToString());
        GetBoosterFromInventory();
        isUsingItem = true;

        EventManager.EmitEvent(EventID.BOOSTER_SELECTED.ToString());
    }

    private void UnuseBooster()
    {
        currentSelectedBoosterID = BoosterID.NONE;
        currentSelectedBooster = null;
        isUsingItem = false;
    }

    private void GetBoosterFromInventory()
    {
        currentSelectedBooster = BoosterManager.Instance.GetBoosterInventorySystem().GetBoosterInInventory(currentSelectedBoosterID);
    }

    public Booster GetCurrentSelectedBooster()
    {
        return currentSelectedBooster;
    }

    public bool IsUsingItem()
    {
        return isUsingItem;
    }
}
