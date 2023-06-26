using UnityEngine;
using TigerForge;
using System.Collections.Generic;
public class ConnectGameplayManager : MonoBehaviour
{
    private static ConnectGameplayManager instance;
    public static ConnectGameplayManager Instance { get => instance; set => instance = value; }

    #region public
    public BallConnectSystem BallConnectSystem { get => ballConnectSystem; set => ballConnectSystem = value; }
    #endregion

    #region private
    [Space(20)]
    [SerializeField] private BallConnectSystem ballConnectSystem;
    [SerializeField] private BallExplosionSystem ballExplosionSystem;
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
    }
}
