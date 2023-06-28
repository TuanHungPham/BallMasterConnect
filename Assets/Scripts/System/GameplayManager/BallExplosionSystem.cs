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
        BallCount = ConnectGameplayManager.Instance.GetBallConnectSystem().GetSelectedBallList().Count;
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

    public void ExplodeAllInRowOrColum(int row = -1, int colum = -1)
    {
        GameObject[,] board = BoardManager.Instance.GetBoardInstance().GetMatrixBoard();

        int dimension = 0;
        if (colum == -1 && row != -1)
        {
            dimension = 1;
        }
        else if (row == -1 && colum != -1)
        {
            dimension = 0;
        }
        else return;

        for (int i = 0; i < board.GetLength(dimension); i++)
        {
            if (dimension == 0)
            {
                Destroy(board[i, colum]);
                continue;
            }
            Destroy(board[row, i]);
        }

        Debug.Log($"Exploded all ball!");

        Invoke(nameof(EmitExplodeEvent), explosionDelay);
    }

    private void EmitExplodeEvent()
    {
        EventManager.EmitEvent(EventID.BALL_EXPLOSION.ToString());
    }

    private bool CanExplode()
    {
        if (BallCount < MINIMUM_BALL_CAN_BE_EXPLODED) return false;

        return true;
    }
}
