using UnityEngine;
using TigerForge;
using System;

public class BallAnimation : MonoBehaviour
{
    #region public
    #endregion

    #region private
    [SerializeField] private BallCtrl ballCtrl;
    [SerializeField] private Animator animator;
    private const string SELECTED_ANIM = "Selected";
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
        ballCtrl = GetComponent<BallCtrl>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.BALL_SELECTING.ToString(), SetSelectedAnimation);
        EventManager.StartListening(EventID.BALL_RELEASING.ToString(), SetSelectedAnimation);
        EventManager.StartListening(EventID.BALL_CONNECTING.ToString(), SetSelectedAnimation);
        EventManager.StartListening(EventID.CLEARING_SELECTED_BALL.ToString(), SetSelectedAnimation);
    }

    private void SetSelectedAnimation()
    {
        animator.SetBool(SELECTED_ANIM, ballCtrl.Ball.IsSelected);
    }
}
