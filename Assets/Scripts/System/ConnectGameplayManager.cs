using UnityEngine;
using TigerForge;
using System.Collections.Generic;
public class ConnectGameplayManager : MonoBehaviour
{
    private static ConnectGameplayManager instance;
    public static ConnectGameplayManager Instance { get => instance; set => instance = value; }

    #region public
    public bool IsDragging { get => isDragging; set => isDragging = value; }
    #endregion

    #region private
    [SerializeField] private bool isDragging;

    [Space(20)]
    [SerializeField] private GameObject firstBall;
    [SerializeField] private GameObject previousBall;
    [SerializeField] private GameObject currentSelectedBall;

    [Space(20)]
    [SerializeField] private List<GameObject> selectedBallList = new List<GameObject>();
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

    }

    private void Start()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.BALL_SELECTING.ToString(), SetBallSelectGameState);
        EventManager.StartListening(EventID.BALL_RELEASING.ToString(), SetBallReleaseGameState);
        EventManager.StartListening(EventID.BALL_CONNECTING.ToString(), ConnectBall);
    }

    private void SetBallReleaseGameState()
    {
        isDragging = false;
        selectedBallList.Clear();
    }

    private void SetBallSelectGameState()
    {
        firstBall = (GameObject)EventManager.GetData(EventID.BALL_SELECTING.ToString());
        selectedBallList.Add(firstBall);

        isDragging = true;
    }

    private void ConnectBall()
    {
        currentSelectedBall = (GameObject)EventManager.GetData(EventID.BALL_CONNECTING.ToString());
        selectedBallList.Add(firstBall);
    }
}
