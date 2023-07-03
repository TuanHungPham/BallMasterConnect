using UnityEngine;
using TigerForge;
using System.Collections.Generic;

public class BallConnectSystem : MonoBehaviour
{
    #region public
    public bool IsDragging { get => isDragging; set => isDragging = value; }
    #endregion

    #region private
    private const float MAX_DISTANCE_CAN_CONNECT = 2.3f;
    private Vector3 lastSelectedBallPos;
    [SerializeField] private bool isDragging;

    [Space(20)]
    [SerializeField] private GameObject firstBall;
    [SerializeField] private GameObject previousBall;
    [SerializeField] private GameObject currentSelectedBall;
    [SerializeField] private GameObject currentPointBall;
    [SerializeField] private GameObject lastSelectedBall;
    [SerializeField] private BallType currentSelectedBallType;
    private MatrixPos lastSelectedBallMatrixPos;

    [Space(20)]
    [SerializeField] private List<GameObject> selectedBallList = new List<GameObject>();
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

    }

    private void Start()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.BALL_SELECTING.ToString(), SelectFirstBall);
        EventManager.StartListening(EventID.BALL_RELEASING.ToString(), ClearAllSelectedBall);
        EventManager.StartListening(EventID.BALL_CONNECTING.ToString(), ConnectBall);
    }

    private void ClearAllSelectedBall()
    {
        isDragging = false;
        selectedBallList.Clear();

        firstBall = null;
        previousBall = null;

        EventManager.EmitEvent(EventID.CLEARING_SELECTED_BALL.ToString());
    }

    private void SelectFirstBall()
    {
        BoosterUsingHandler handler = BoosterManager.Instance.GetBoosterUsingHandler();
        if (handler.IsUsingBooster()) return;

        firstBall = (GameObject)EventManager.GetData(EventID.BALL_SELECTING.ToString());

        Ball ballScript = firstBall.GetComponent<Ball>();

        currentSelectedBallType = ballScript.BallType;
        currentSelectedBall = firstBall;
        ballScript.SelectBall();

        selectedBallList.Add(firstBall);

        isDragging = true;
    }

    private void ConnectBall()
    {
        currentPointBall = (GameObject)EventManager.GetData(EventID.BALL_CONNECTING.ToString());
        Ball ballScript = currentPointBall.GetComponent<Ball>();

        if (!IsSameBallType(ballScript) || !IsInDistance() || currentPointBall == currentSelectedBall) return;

        if (currentPointBall == previousBall)
        {
            BackThePreviousBallConnection();
            return;
        }

        if (selectedBallList.Contains(currentPointBall)) return;

        ConnectTheCurrentBall(ballScript);

        lastSelectedBall = currentSelectedBall;

        SetLastSelectedBallPos();
        SetLastSelectedBallMatrixPos();
    }

    private void SetLastSelectedBallPos()
    {
        lastSelectedBallPos = lastSelectedBall.transform.position;
    }

    private void SetLastSelectedBallMatrixPos()
    {
        Ball ball = lastSelectedBall.GetComponent<Ball>();
        lastSelectedBallMatrixPos = ball.GetBallMatrixPosition();
    }

    private void ConnectTheCurrentBall(Ball ballScript)
    {
        selectedBallList.Add(currentPointBall);
        ballScript.SelectBall();

        currentSelectedBall = currentPointBall;
        int index = selectedBallList.IndexOf(currentPointBall);
        previousBall = selectedBallList[index - 1];
    }

    private void BackThePreviousBallConnection()
    {
        if (previousBall == firstBall)
        {
            ClearAllSelectedBall();
            return;
        }

        int index = 0;
        Ball newBallScript = currentSelectedBall.GetComponent<Ball>();
        newBallScript.DeselectBall();

        selectedBallList.Remove(currentSelectedBall);

        currentSelectedBall = previousBall;

        index = selectedBallList.IndexOf(currentPointBall);
        previousBall = selectedBallList[index - 1];
    }

    private bool IsSameBallType(Ball ballScript)
    {
        if (ballScript.BallType == currentSelectedBallType) return true;

        return false;
    }

    private bool IsInDistance()
    {
        float distance = Vector2.Distance(currentPointBall.transform.position, currentSelectedBall.transform.position);

        if (distance <= MAX_DISTANCE_CAN_CONNECT) return true;

        return false;
    }

    public List<GameObject> GetSelectedBallList()
    {
        return selectedBallList;
    }

    public Vector3 GetLastSelectedBallPos()
    {
        return lastSelectedBallPos;
    }

    public MatrixPos GetLastSelectedBallMatrixPos()
    {
        return lastSelectedBallMatrixPos;
    }
}
