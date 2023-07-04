using UnityEngine;
using TigerForge;
using System;

public class BallCount : MonoBehaviour
{
    #region public
    #endregion

    #region private
    [SerializeField] private int ball1Count;
    [SerializeField] private int ball2Count;
    [SerializeField] private int ball3Count;
    [SerializeField] private int ball4Count;
    [SerializeField] private int totalBallCount;
    #endregion

    private void Start()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.BALL_EXPLODED.ToString(), CountExplodedBall);
    }

    private void CountExplodedBall()
    {
        BallType ballType = (BallType)EventManager.GetData(EventID.BALL_EXPLODED.ToString());

        totalBallCount++;

        switch (ballType)
        {
            case BallType.BALL_1:
                ball1Count++;
                break;
            case BallType.BALL_2:
                ball2Count++;
                break;
            case BallType.BALL_3:
                ball3Count++;
                break;
            case BallType.BALL_4:
                ball4Count++;
                break;
            default:
                break;
        }
    }
}
