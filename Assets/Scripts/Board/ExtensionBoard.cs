using UnityEngine;

public class ExtensionBoard : Board
{
    #region public
    #endregion

    #region private
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Reset()
    {
        base.Reset();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    private void Start()
    {
        InitializeNewBoard();
    }

    private void Update()
    {
        HandleShiftBoardDown();
    }

    protected override void InitializeNewBoard()
    {
        base.InitializeNewBoard();
    }

    protected override void SetMatrixPos(GameObject holder, int row, int colum)
    {
        base.SetMatrixPos(holder, row, colum);
    }

    private void HandleShiftBoardDown()
    {
        foreach (var holder in ballHolderList)
        {
            if (!holder.IsEmpty) continue;

            MatrixPos currentHolderMatrixPos = holder.matrixPos;
            if (currentHolderMatrixPos.row == 0)
            {
                // holder.CreateRandomBall();
                continue;
            }

            int aboveRow = currentHolderMatrixPos.row - 1;

            GameObject aboveHolder = mainBoardMatrix[aboveRow, currentHolderMatrixPos.colum];
            BallHolder aboveBallHolder = aboveHolder.GetComponent<BallHolder>();

            if (aboveBallHolder.IsEmpty) continue;

            ShiftBallInCurrentBoard(holder, aboveBallHolder);
        }
    }

    protected override void ShiftBallInCurrentBoard(BallHolder currentHolder, BallHolder aboveBallHolder)
    {
        base.ShiftBallInCurrentBoard(currentHolder, aboveBallHolder);
    }
}
