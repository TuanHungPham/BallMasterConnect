using System;
using UnityEngine;
using TigerForge;

public class BoosterUsingHandler : MonoBehaviour
{
    #region public
    #endregion

    #region private
    [SerializeField] private Transform pointToUseBooster;
    private MatrixPos matrixPosToUseBooster;
    #endregion

    private void Awake()
    {
        LoadComponents();
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.BOOSTER_SELECTED.ToString(), UseBooster);
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {

    }

    private void UseBooster()
    {
        Booster currentSelectedBooster = BoosterManager.Instance.GetBoosterSelectingHandler().GetCurrentSelectedBooster();

        if (currentSelectedBooster.GetBoosterData().BoosterType == BoosterType.PLACING_BOOSTER)
        {
            PlaceBooster(currentSelectedBooster);
        }
        else if (currentSelectedBooster.GetBoosterData().BoosterType == BoosterType.CASTING_BOOSTER)
        {
            CastBoosterFunction(currentSelectedBooster);
        }
    }

    private void CastBoosterFunction(Booster booster)
    {
        Debug.Log("Casting Booster...!");
    }

    private void PlaceBooster(Booster booster)
    {
        Debug.Log("Placing Booster...!");
    }
}
