using UnityEngine;
using TigerForge;
using System.Collections.Generic;

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
        rocketBoostPrefab = Resources.Load<GameObject>("Prefabs/RocketBoost");
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
    }

    private void GetStarBonus()
    {
        if (numberOfExplodedBall < minimumForStarBonus) return;

        starBonus = numberOfExplodedBall;

        Debug.Log($"You've just rewarded {starBonus} Stars!");
    }

    private void GetRocketBonus()
    {
        if (numberOfExplodedBall < minimumForRocket) return;

        CreateRocketBoost();
        Debug.Log("Rocket incoming...");
    }

    private void CreateRocketBoost()
    {
        GameObject lastSelectedBall = ConnectGameplayManager.Instance.GetBallConnectSystem().GetLastSelectedBall();

        GameObject rocketBoost = Instantiate(rocketBoostPrefab);
        rocketBoost.transform.position = lastSelectedBall.transform.position;
    }

    private void RocketBoostFunction(int row = -1, int colum = -1)
    {
        ConnectGameplayManager.Instance.GetBallExplosionSystem().ExplodeAllInRowOrColum(row, colum);
    }
}
