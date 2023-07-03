using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Shuffle : BoosterFunction
{
    #region public
    #endregion

    #region private
    private const float END_MOVING_ROUTE_DISTANCE = 15f;
    private const float MOVING_TIME = 0.1f;
    #endregion

    protected override void LoadComponents()
    {
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override IEnumerator HandleBoosterFunction()
    {
        EmitUsingBoosterEvent();
        SetBallMovingRoute(true);

        yield return new WaitForSeconds(MOVING_TIME);

        HandleShuffleFunction();
        SetBallMovingRoute(false);
        SelfDestruct();
    }

    private void SetBallMovingRoute(bool isMovingOut)
    {
        List<BallHolder> ballHoldersList = BoardManager.Instance.GetBoardInstance().GetBallHolderList();
        MatrixPos matrixPos = new MatrixPos();
        GameObject ball = null;

        float moveDirection = 1;

        if (!isMovingOut)
        {
            moveDirection = -1;
        }

        foreach (BallHolder ballHolder in ballHoldersList)
        {
            matrixPos = ballHolder.matrixPos;
            ball = ballHolder.GetCurrentBallHolding();

            if (matrixPos.row % 2 == 0)
            {
                ball.transform.DOMoveX(ball.transform.position.x + END_MOVING_ROUTE_DISTANCE * moveDirection, MOVING_TIME);
            }
            else
            {
                ball.transform.DOMoveX(ball.transform.position.x - END_MOVING_ROUTE_DISTANCE * moveDirection, MOVING_TIME);
            }
        }
    }

    private void HandleShuffleFunction()
    {
        List<BallHolder> ballHoldersList = BoardManager.Instance.GetBoardInstance().GetBallHolderList();

        foreach (BallHolder ballHolder in ballHoldersList)
        {
            ballHolder.ClearBallHolding();
            ballHolder.CreateRandomBall(ballHolder.transform.GetChild(0).position);
        }
    }

    protected override void SelfDestruct()
    {
        base.SelfDestruct();
    }

    protected override void EmitUsingBoosterEvent()
    {
        base.EmitUsingBoosterEvent();
    }
}
