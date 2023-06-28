using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TigerForge;

public class Board : MonoBehaviour
{
    #region public
    #endregion

    #region private
    private const int ROW = 6;
    private const int COLUM = 6;
    [SerializeField] private float ballDropTime;
    [SerializeField] private float firstPos;
    [SerializeField] private float spacingX;
    [SerializeField] private float spacingY;
    [SerializeField] private GameObject ballHolderPrefab;
    [SerializeField] private GameObject[,] mainBoardMatrix = new GameObject[ROW, COLUM];
    [SerializeField] private List<BallHolder> ballHolderList = new List<BallHolder>();
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
        ballHolderPrefab = Resources.Load<GameObject>("Prefabs/BallHolder");

        ballDropTime = 0.02f;
        firstPos = transform.position.x;
        spacingX = 1.6f;
        spacingY = 1.6f;
    }

    private void Start()
    {
        InitializeNewBoard();
    }

    private void Update()
    {
        HandleShiftBoardDown();
    }

    private void InitializeNewBoard()
    {
        Vector3 spawnPoint = transform.position;

        for (int i = 0; i < ROW; i++)
        {
            for (int j = 0; j < COLUM; j++)
            {
                GameObject holder = Instantiate(ballHolderPrefab);
                float postitionX = firstPos + j * spacingX;
                spawnPoint.x = postitionX;
                holder.transform.position = spawnPoint;
                holder.transform.SetParent(transform);

                mainBoardMatrix[i, j] = holder;
                SetMatrixPos(holder, i, j);
            }

            spawnPoint.y -= spacingY;
        }
    }

    private void SetMatrixPos(GameObject holder, int row, int colum)
    {
        BallHolder ballHolder = holder.GetComponent<BallHolder>();
        if (ballHolder == null) return;

        ballHolderList.Add(ballHolder);

        ballHolder.matrixPos.SetMatrixPos(row, colum);
        holder.name = $"[{ballHolder.matrixPos.row},{ballHolder.matrixPos.colum}]";
    }

    private void HandleShiftBoardDown()
    {
        foreach (var holder in ballHolderList)
        {
            if (!holder.IsEmpty) continue;

            MatrixPos currentHolderMatrixPos = holder.matrixPos;
            if (currentHolderMatrixPos.row == 0)
            {
                holder.CreateRandomBall(holder.transform.position, true);
                continue;
            }

            int aboveRow = currentHolderMatrixPos.row - 1;

            GameObject aboveHolder = mainBoardMatrix[aboveRow, currentHolderMatrixPos.colum];
            BallHolder aboveBallHolder = aboveHolder.GetComponent<BallHolder>();

            if (aboveBallHolder.IsEmpty) continue;

            ShiftBallInCurrentBoard(holder, aboveBallHolder);
        }
    }

    private void ShiftBallInCurrentBoard(BallHolder currentHolder, BallHolder aboveBallHolder)
    {
        currentHolder.SetCurrentBallHolding(aboveBallHolder.GetCurrentBallHolding());
        currentHolder.IsEmpty = false;

        aboveBallHolder.GetCurrentBallHolding().transform.SetParent(currentHolder.transform);
        currentHolder.GetCurrentBallHolding().transform.DOMoveY(currentHolder.transform.position.y, ballDropTime);

        aboveBallHolder.SetCurrentBallHolding(null);
        aboveBallHolder.IsEmpty = true;

        EmitShiftBoardDownEvent();
    }

    private void EmitShiftBoardDownEvent()
    {
        EventManager.EmitEvent(EventID.BOARD_SHIFTING_DOWN.ToString());
    }

    public GameObject[,] GetMatrixBoard()
    {
        return mainBoardMatrix;
    }
}
