using UnityEngine;
using TigerForge;

public class BallExplosionSystem : MonoBehaviour
{
    #region public
    public int BallCount { get => ballCount; private set => ballCount = value; }
    #endregion

    #region private
    private const int MINIMUM_BALL_CAN_BE_EXPLODED = 3;
    [SerializeField] private float explosionDelay;
    [SerializeField] private int ballCount;
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
        EventManager.StartListening(EventID.BALL_RELEASING.ToString(), CheckBallExplosion);
    }

    private void CheckBallExplosion()
    {
        ballCount = ConnectGameplayManager.Instance.GetBallConnectSystem().GetSelectedBallList().Count;
        if (!CanExplode()) return;

        ExplodeAllConnectedBall();
    }

    private void ExplodeAllConnectedBall()
    {
        foreach (var ball in ConnectGameplayManager.Instance.GetBallConnectSystem().GetSelectedBallList())
        {
            Destroy(ball);
        }

        Invoke(nameof(EmitExplodeEvent), explosionDelay);
    }

    public void ExplodeAllBallInRow(int row)
    {
        GameObject[,] board = BoardManager.Instance.GetBoardInstance().GetMatrixBoard();
        GameObject ball = null;

        for (int i = 0; i < board.GetLength(1); i++)
        {
            if (board[row, i].transform.childCount == 0) continue;

            ball = board[row, i].transform.GetChild(0).gameObject;
            Destroy(ball);
        }

        Invoke(nameof(EmitExplodeEvent), explosionDelay);
    }

    public void ExplodeAllBallInColum(int colum)
    {
        GameObject[,] board = BoardManager.Instance.GetBoardInstance().GetMatrixBoard();
        GameObject ball = null;

        for (int i = 0; i < board.GetLength(0); i++)
        {
            if (board[i, colum].transform.childCount == 0) continue;

            ball = board[i, colum].transform.GetChild(0).gameObject;
            Destroy(ball);
        }

        Invoke(nameof(EmitExplodeEvent), explosionDelay);
    }

    public void ExplodeAroundPosition(int row, int colum)
    {
        GameObject[,] board = BoardManager.Instance.GetBoardInstance().GetMatrixBoard();
        GameObject ball = null;

        for (int i = row - 1; i <= row + 1; i++)
        {
            if (i < 0 || i >= board.GetLength(0)) continue;

            for (int j = colum - 1; j <= colum + 1; j++)
            {
                if (j < 0 || j >= board.GetLength(1)) continue;

                if (board[i, j].transform.childCount == 0) return;

                ball = ball = board[i, j].transform.GetChild(0).gameObject;
                Destroy(ball);
            }
        }

        Invoke(nameof(EmitExplodeEvent), explosionDelay);
    }

    private void EmitExplodeEvent()
    {
        EventManager.EmitEvent(EventID.BALL_EXPLOSION.ToString());
        ballCount = 0;
    }

    private bool CanExplode()
    {
        if (ballCount < MINIMUM_BALL_CAN_BE_EXPLODED) return false;

        return true;
    }
}
