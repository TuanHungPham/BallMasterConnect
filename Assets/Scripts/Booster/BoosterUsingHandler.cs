using UnityEngine;
using TigerForge;
using System;

public class BoosterUsingHandler : MonoBehaviour
{
    #region public
    #endregion

    #region private
    [SerializeField] private bool isUsingBooster;

    [Space(20)]
    [SerializeField] private Vector3 usingPoint;
    [SerializeField] private GameObject darkScreen;
    [SerializeField] private Transform boosterPool;
    [SerializeField] private Booster currentUsingBooster;
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
        EventManager.StartListening(EventID.BOOSTER_DESELECTING.ToString(), UnchoosePlacement);
        EventManager.StartListening(EventID.BALL_SELECTING.ToString(), PlaceBooster);
        EventManager.StartListening(EventID.BOOSTER_USING.ToString(), ResetUsingFunction);
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        darkScreen = GameObject.Find("DarkScreen");
        boosterPool = GameObject.Find("BoosterPool").transform;
    }

    private void Start()
    {
        ResetUsingFunction();
    }

    private void UseBooster()
    {
        currentUsingBooster = BoosterManager.Instance.GetBoosterSelectingHandler().GetCurrentSelectedBooster();

        if (currentUsingBooster.GetBoosterData().BoosterType == BoosterType.PLACING_BOOSTER)
        {
            ChoosePlacement();
        }
        else if (currentUsingBooster.GetBoosterData().BoosterType == BoosterType.CASTING_BOOSTER)
        {
            UseCastingBooster(currentUsingBooster);
        }
    }

    private void UseCastingBooster(Booster booster)
    {
        Debug.Log("Casting Booster...!");

        isUsingBooster = true;
        CreateBoosterFunction(booster, transform.position, boosterPool);
    }

    private void PlaceBooster()
    {
        if (currentUsingBooster == null) return;

        Debug.Log("Placing Booster...");

        GameObject choosedBall = (GameObject)EventManager.GetData(EventID.BALL_SELECTING.ToString());
        Ball ballScript = choosedBall.GetComponent<Ball>();

        usingPoint = choosedBall.transform.position;
        matrixPosToUseBooster = ballScript.GetBallMatrixPosition();

        isUsingBooster = true;

        CreateBoosterFunction(currentUsingBooster, transform.position, boosterPool);
    }

    private void CreateBoosterFunction(Booster booster, Vector3 position, Transform parent)
    {
        GameObject boosterFunction = Instantiate(booster.GetBoosterData().BoosterPrefab);
        boosterFunction.transform.position = position;
        boosterFunction.transform.SetParent(parent, true);
    }

    private void ResetUsingFunction()
    {
        currentUsingBooster = null;
        isUsingBooster = false;
        SetDarkScreen(false);
    }


    private void ChoosePlacement()
    {
        Debug.Log("Choosing Booster placement...");
        SetDarkScreen(true);
    }

    private void UnchoosePlacement()
    {
        ResetUsingFunction();
    }

    private void SetDarkScreen(bool set)
    {
        darkScreen.SetActive(set);
    }

    private void EmitUsingBoosterEvent()
    {
        EventManager.EmitEvent(EventID.BOOSTER_USING.ToString());
    }

    public Booster GetCurrentUsingBooster()
    {
        return currentUsingBooster;
    }

    public Vector3 GetUsingPoint()
    {
        return usingPoint;
    }

    public MatrixPos GetUsingMatrixPos()
    {
        return matrixPosToUseBooster;
    }

    public bool IsUsingBooster()
    {
        return isUsingBooster;
    }
}
