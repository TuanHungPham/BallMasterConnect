using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Board : MonoBehaviour
{
    #region public
    #endregion

    #region private
    protected const int ROW = 6;
    protected const int COLUM = 6;
    [SerializeField] protected float ballDropTime;
    [SerializeField] protected float firstPos;
    [SerializeField] protected float spacingX;
    [SerializeField] protected float spacingY;
    [SerializeField] protected GameObject ballHolderPrefab;
    [SerializeField] protected GameObject[,] mainBoardMatrix = new GameObject[ROW, COLUM];
    [SerializeField] protected List<BallHolder> ballHolderList = new List<BallHolder>();
    #endregion

    protected virtual void Awake()
    {
        LoadComponents();
    }

    protected virtual void Reset()
    {
        LoadComponents();
    }

    protected virtual void LoadComponents()
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

    protected virtual void InitializeNewBoard()
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

    protected virtual void SetMatrixPos(GameObject holder, int row, int colum)
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

    protected virtual void ShiftBallInCurrentBoard(BallHolder currentHolder, BallHolder aboveBallHolder)
    {
        currentHolder.CurrentBallHolding = aboveBallHolder.CurrentBallHolding;
        currentHolder.IsEmpty = false;

        aboveBallHolder.CurrentBallHolding.transform.SetParent(currentHolder.transform);
        currentHolder.CurrentBallHolding.transform.DOMoveY(currentHolder.transform.position.y, ballDropTime);

        aboveBallHolder.CurrentBallHolding = null;
        aboveBallHolder.IsEmpty = true;
    }
}
