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
    #endregion

    private void Start()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.CLEARING_SELECTED_BALL.ToString(), DeselectBall);
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
