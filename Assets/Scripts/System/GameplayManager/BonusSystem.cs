using UnityEngine;
using TigerForge;

public class BonusSystem : MonoBehaviour
{
    #region public
    #endregion

    #region private
    [SerializeField] private int numberOfExplodedBall;
    [SerializeField] private int starBonus;
    [SerializeField] private int minimumForStarBonus;
    [SerializeField] private int minimumForRocket;

    [Space(20)]
    [SerializeField] private GameObject rocketBoostPrefab;
    [SerializeField] private Transform boosterPool;
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
        rocketBoostPrefab = Resources.Load<GameObject>("Prefabs/Booster/RocketBooster");
        boosterPool = GameObject.Find("BoosterPool").transform;
    }

    private void Start()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.BALL_EXPLOSION.ToString(), CheckBonus);
    }

    private void CheckBonus()
    {
        GetNumberOfExplodedBall();
        GetStarBonus();
        GetRocketBonus();
    }

    private void GetNumberOfExplodedBall()
    {
        numberOfExplodedBall = ConnectGameplayManager.Instance.GetBallExplosionSystem().BallCount;
        // Debug.Log("Number of Exploded Ball: " + numberOfExplodedBall);
    }

    private void GetStarBonus()
    {
        if (numberOfExplodedBall < minimumForStarBonus) return;

        starBonus = numberOfExplodedBall;

        // Debug.Log($"You've just rewarded {starBonus} Stars!");
    }

    private void GetRocketBonus()
    {
        if (numberOfExplodedBall < minimumForRocket) return;

        CreateRocketBoost();

        numberOfExplodedBall = 0;
    }

    private void CreateRocketBoost()
    {
        Vector3 lastSelectedBallPosition = ConnectGameplayManager.Instance.GetBallConnectSystem().GetLastSelectedBallPos();

        GameObject rocketBoost = Instantiate(rocketBoostPrefab);
        rocketBoost.transform.position = lastSelectedBallPosition;
        rocketBoost.transform.SetParent(boosterPool, true);

        Invoke(nameof(ExplodeByRocket), DelayTimeSystem.ROCKET_BOOST_FUNCTION_DELAY);
    }

    private void ExplodeByRocket()
    {
        MatrixPos matrixPos = ConnectGameplayManager.Instance.GetBallConnectSystem().GetLastSelectedBallMatrixPos();
        // Debug.Log("Row: " + matrixPos.row);
        // Debug.Log("Colum: " + matrixPos.colum);

        ConnectGameplayManager.Instance.GetBallExplosionSystem().ExplodeAllBallInRow(matrixPos.row);
        ConnectGameplayManager.Instance.GetBallExplosionSystem().ExplodeAllBallInColum(matrixPos.colum);
    }
}
