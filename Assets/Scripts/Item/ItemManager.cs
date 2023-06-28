using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private static ItemManager instance;
    public static ItemManager Instance { get => instance; private set => instance = value; }

    #region public
    #endregion

    #region private
    [SerializeField] private ItemID currentSelectedItemID;
    [SerializeField] private GameObject pointToUseItem;
    [SerializeField] private bool isUsingItem;
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

    }
}
