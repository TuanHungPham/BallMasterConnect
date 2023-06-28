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
    [SerializeField] private BallType ballType;
    [SerializeField] private BallHolder currentBallHolder;
    private MatrixPos matrixPos;
    #endregion

    private void Start()
    {
        ListenEvent();
        SetBallHolderComponent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.CLEARING_SELECTED_BALL.ToString(), DeselectBall);
        EventManager.StartListening(EventID.BOARD_SHIFTING_DOWN.ToString(), SetBallHolderComponent);
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

    public MatrixPos GetBallMatrixPosition()
    {
        return matrixPos;
    }

    public void SelectBall()
    {
        IsSelected = true;
    }

    public void DeselectBall()
    {
        IsSelected = false;
    }

    private void OnMouseDown()
    {
        EventManager.SetData(EventID.BALL_SELECTING.ToString(), this.gameObject);
        EventManager.EmitEvent(EventID.BALL_SELECTING.ToString());

        Debug.Log($"Clicking at {gameObject.name}");
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
}
