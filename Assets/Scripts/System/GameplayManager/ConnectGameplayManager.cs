using UnityEngine;

public class ConnectGameplayManager : MonoBehaviour
{
    private static ConnectGameplayManager instance;
    public static ConnectGameplayManager Instance { get => instance; set => instance = value; }

    #region public
    #endregion

    #region private
    [Space(20)]
    [SerializeField] private BallConnectSystem ballConnectSystem;
    [SerializeField] private BallExplosionSystem ballExplosionSystem;
    [SerializeField] private BonusSystem bonusSystem;
    #endregion

    private void Awake()
    {
        HandleInstance();
        LoadComponents();
    }

    private void HandleInstance()
    {
        instance = this;
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        ballConnectSystem = GetComponentInChildren<BallConnectSystem>();
        ballExplosionSystem = GetComponentInChildren<BallExplosionSystem>();
        bonusSystem = GetComponentInChildren<BonusSystem>();
    }

    public BallConnectSystem GetBallConnectSystem()
    {
        return ballConnectSystem;
    }

    public BallExplosionSystem GetBallExplosionSystem()
    {
        return ballExplosionSystem;
    }
}
