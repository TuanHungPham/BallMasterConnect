using UnityEngine;

public class Booster : MonoBehaviour
{
    #region public
    #endregion

    #region private
    [SerializeField] private BoosterData boosterData;
    [SerializeField] private int boosterQuantity;
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
        boosterData = Resources.Load<BoosterData>($"Data/{gameObject.name}");
    }

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
