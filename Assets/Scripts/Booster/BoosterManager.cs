using UnityEngine;

public class BoosterManager : MonoBehaviour
{
    private static BoosterManager instance;
    public static BoosterManager Instance { get => instance; private set => instance = value; }

    #region public
    #endregion

    #region private
    [SerializeField] private BoosterSelectingHandler boosterSelectingHandler;
    [SerializeField] private BoosterUsingHandler boosterUsingHandler;
    [SerializeField] private BoosterInventory boosterInventory;
    #endregion

    private void Awake()
    {
        HandleInstanceObj();
        LoadComponents();
    }

    private void HandleInstanceObj()
    {
        instance = this;
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        boosterInventory = GetComponentInChildren<BoosterInventory>();
        boosterSelectingHandler = GetComponentInChildren<BoosterSelectingHandler>();
        boosterUsingHandler = GetComponentInChildren<BoosterUsingHandler>();
    }

    public BoosterInventory GetBoosterInventorySystem()
    {
        return boosterInventory;
    }

    public BoosterSelectingHandler GetBoosterSelectingHandler()
    {
        return boosterSelectingHandler;
    }

    public BoosterUsingHandler GetBoosterUsingHandler()
    {
        return boosterUsingHandler;
    }

}
