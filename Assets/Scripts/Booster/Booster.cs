using UnityEngine;
using System;

[Serializable]
public class Booster
{
    #region public
    #endregion

    #region private
    [SerializeField] private BoosterData boosterData;
    [SerializeField] private int boosterQuantity;
    #endregion

    public void AddMoreBooster(int quantity)
    {
        boosterQuantity += quantity;
    }

    public void ConsumpBooster()
    {
        boosterQuantity--;
    }

    public int GetBoosterQuantity()
    {
        return boosterQuantity;
    }

    public BoosterData GetBoosterData()
    {
        return boosterData;
    }
}
