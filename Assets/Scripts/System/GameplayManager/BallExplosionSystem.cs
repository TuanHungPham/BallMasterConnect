using UnityEngine;
using TigerForge;

public class BallExplosionSystem : MonoBehaviour
{
    #region public
    #endregion

    #region private
    private const int MINIMUM_BALL_CAN_BE_EXPLODED = 3;
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
        ballCount = ConnectGameplayManager.Instance.BallConnectSystem.SelectedBallList.Count;
        if (!CanExplode()) return;

        ExplodeAllConnectedBall();
    }

    private void ExplodeAllConnectedBall()
    {
        foreach (var ball in ConnectGameplayManager.Instance.BallConnectSystem.SelectedBallList)
        {
            Destroy(ball);
        }
        Debug.Log("All Ball are exploded!");

        Invoke(nameof(EmitExplodeEvent), 1);
    }

    private void EmitExplodeEvent()
    {
        EventManager.EmitEvent(EventID.BALL_EXPLOSION.ToString());
    }

    private bool CanExplode()
    {
        if (ballCount < MINIMUM_BALL_CAN_BE_EXPLODED) return false;

        return true;
    }
}
