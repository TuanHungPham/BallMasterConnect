using UnityEngine;
using TigerForge;
using System;

public class Ball : MonoBehaviour
{
    #region public
    public bool IsSelected { get => isSelected; set => isSelected = value; }
    public BallType BallType { get => ballType; set => ballType = value; }
    #endregion

    #region private
    [SerializeField] private bool isSelected;

    [Space(20)]
    [SerializeField] private SpriteRenderer ballSprite;
    [SerializeField] private BallType ballType;
    [SerializeField] private BallHolder currentBallHolder;
    private MatrixPos matrixPos;
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
        ballSprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        ListenEvent();
        SetBallHolderComponent();
        HideBall();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.CLEARING_SELECTED_BALL.ToString(), DeselectBall);
        EventManager.StartListening(EventID.BOARD_SHIFTING_DOWN.ToString(), SetBallHolderComponent);
        EventManager.StartListening(EventID.BALL_RELEASING.ToString(), HideBall);
    }

    private void SetBallHolderComponent()
    {
        currentBallHolder = transform.parent.GetComponent<BallHolder>();
        SetBallMatrixPosition();
    }

    private void SetBallMatrixPosition()
    {
        matrixPos.SetMatrixPos(currentBallHolder.matrixPos.row, currentBallHolder.matrixPos.colum);
    }

    public void ShowBall()
    {
        ballSprite.sortingOrder = 1;
    }

    public void HideBall()
    {
        ballSprite.sortingOrder = -1;
    }

    public void SelectBall()
    {
        IsSelected = true;
    }

    public void DeselectBall()
    {
        IsSelected = false;
    }

    public MatrixPos GetBallMatrixPosition()
    {
        return matrixPos;
    }

    public BallType GetBallType()
    {
        return ballType;
    }

    private void OnMouseDown()
    {
        EventManager.SetData(EventID.BALL_SELECTING.ToString(), this.gameObject);
        EventManager.EmitEvent(EventID.BALL_SELECTING.ToString());
    }

    private void OnMouseUp()
    {
        EventManager.EmitEvent(EventID.BALL_RELEASING.ToString());
    }

    private void OnMouseEnter()
    {
        if (!ConnectGameplayManager.Instance.GetBallConnectSystem().IsDragging) return;

        EventManager.SetData(EventID.BALL_CONNECTING.ToString(), this.gameObject);
        EventManager.EmitEvent(EventID.BALL_CONNECTING.ToString());
    }

    private void OnDestroy()
    {
        EventManager.SetData(EventID.BALL_EXPLODED.ToString(), ballType);
        EventManager.EmitEvent(EventID.BALL_EXPLODED.ToString());
    }
}
