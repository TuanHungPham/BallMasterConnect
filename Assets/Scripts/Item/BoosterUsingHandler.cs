using UnityEngine;
using TigerForge;

public class BoosterUsingHandler : MonoBehaviour
{
    #region public
    #endregion

    #region private
    [SerializeField] private BoosterID currentSelectedItemID;
    [SerializeField] private GameObject pointToUseItem;
    [SerializeField] private bool isUsingItem;
    private MatrixPos matrixPosToUseItem;
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

    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.BOOSTER_SELECTING.ToString(), SelectUsingBooster);
        EventManager.StartListening(EventID.BOOSTER_DESELECTING.ToString(), UnuseBooster);
    }

    private void SelectUsingBooster()
    {
        currentSelectedItemID = (BoosterID)EventManager.GetData(EventID.BALL_SELECTING.ToString());
    }

    private void UnuseBooster()
    {
        isUsingItem = false;
    }

    public MatrixPos GetMatrixPosToUseItem()
    {
        return matrixPosToUseItem;
    }
}
