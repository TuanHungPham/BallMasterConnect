using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class HintSystem : MonoBehaviour
{
    #region public
    #endregion

    #region private
    [SerializeField] private GameObject darkScreen;

    [Space(20)]
    [SerializeField] private List<Ball> listOfHintBall = new List<Ball>();
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
        darkScreen = GameObject.Find("DarkScreen");
    }

    private void Start()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.BALL_SELECTING.ToString(), ShowHintScreen);
        EventManager.StartListening(EventID.BALL_RELEASING.ToString(), DisableHintScreen);
    }

    private void ShowHintScreen()
    {
        SetActiveDarkScreen(true);
        ShowHint();
    }

    private void ShowHint()
    {
        GameObject firstSelectedBall = (GameObject)EventManager.GetData(EventID.BALL_SELECTING.ToString());
        Ball firstBall = firstSelectedBall.GetComponent<Ball>();
        GameObject[,] matrixBoard = BoardManager.Instance.GetBoardInstance().GetMatrixBoard();

        CheckHintBall(firstBall, matrixBoard);
    }

    private void CheckHintBall(Ball firstBall, GameObject[,] board)
    {
        firstBall.ShowBall();
        listOfHintBall.Add(firstBall);

        MatrixPos ballMatrixPos = firstBall.GetBallMatrixPosition();

        for (int i = ballMatrixPos.row - 1; i <= ballMatrixPos.row + 1; i++)
        {
            if (i < 0 || i >= board.GetLength(0)) continue;

            for (int j = ballMatrixPos.colum - 1; j <= ballMatrixPos.colum + 1; j++)
            {
                if (j < 0 || j >= board.GetLength(1)) continue;

                BallHolder ballHolder = board[i, j].GetComponent<BallHolder>();
                if (ballHolder.GetCurrentBallHolding() == null) continue;

                Ball currentCheckBall = ballHolder.GetCurrentBallHolding().GetComponent<Ball>();

                if (currentCheckBall.GetBallType() == firstBall.GetBallType() && !listOfHintBall.Contains(currentCheckBall))
                {
                    listOfHintBall.Add(currentCheckBall);
                    currentCheckBall.ShowBall();

                    CheckHintBall(currentCheckBall, board);
                }
            }
        }
    }

    private void ShowBallOnHintScreen(Ball ballScript)
    {
        ballScript.ShowBall();
    }

    private void DisableHintScreen()
    {
        SetActiveDarkScreen(false);

        listOfHintBall.Clear();
    }

    private void SetActiveDarkScreen(bool set)
    {
        darkScreen.SetActive(set);
    }
}
