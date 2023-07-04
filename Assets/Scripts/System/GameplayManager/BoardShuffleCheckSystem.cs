using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class BoardShuffleCheckSystem : MonoBehaviour
{
    #region public
    #endregion

    #region private
    [SerializeField] private List<Ball> listOfBallCanConnect = new List<Ball>();
    [SerializeField] private bool haveToShuffle;
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
        EventManager.StartListening(EventID.BOARD_SHIFTING_DOWN.ToString(), CheckBall);
    }

    private void CheckBall()
    {
        GameObject[,] matrixBoard = BoardManager.Instance.GetBoardInstance().GetMatrixBoard();
        List<BallHolder> ballHolderList = BoardManager.Instance.GetBoardInstance().GetBallHolderList();

        int wayCount = 0;

        foreach (BallHolder ballHolder in ballHolderList)
        {
            listOfBallCanConnect.Clear();
            Ball ballCheck = ballHolder.GetCurrentBallHolding().GetComponent<Ball>();
            CheckBallCanConnect(ballCheck, matrixBoard);

            if (listOfBallCanConnect.Count < 3) continue;

            foreach (Ball ball in listOfBallCanConnect)
            {
                ball.ShowBall();
            }
            wayCount++;
        }

        Debug.Log("Way Count: " + wayCount);

        if (wayCount > 0)
        {
            haveToShuffle = false;
            return;
        }

        haveToShuffle = true;
    }

    private void CheckBallCanConnect(Ball ballCheck, GameObject[,] board)
    {
        listOfBallCanConnect.Add(ballCheck);

        MatrixPos ballMatrixPos = ballCheck.GetBallMatrixPosition();

        for (int i = ballMatrixPos.row - 1; i <= ballMatrixPos.row + 1; i++)
        {
            if (i < 0 || i >= board.GetLength(0)) continue;

            for (int j = ballMatrixPos.colum - 1; j <= ballMatrixPos.colum + 1; j++)
            {
                if (j < 0 || j >= board.GetLength(1)) continue;

                BallHolder ballHolder = board[i, j].GetComponent<BallHolder>();
                if (ballHolder.GetCurrentBallHolding() == null) continue;

                Ball currentCheckBall = ballHolder.GetCurrentBallHolding().GetComponent<Ball>();

                if (currentCheckBall.GetBallType() == ballCheck.GetBallType() && !listOfBallCanConnect.Contains(currentCheckBall))
                {
                    listOfBallCanConnect.Add(currentCheckBall);

                    CheckBallCanConnect(currentCheckBall, board);
                }
            }
        }
    }
}
