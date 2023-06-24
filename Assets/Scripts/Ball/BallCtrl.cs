using UnityEngine;

public class BallCtrl : MonoBehaviour
{
    #region public
    public Ball Ball { get => ball; set => ball = value; }
    public BallAnimation BallAnimation { get => ballAnimation; set => ballAnimation = value; }
    #endregion

    #region private
    [SerializeField] private Ball ball;
    [SerializeField] private BallAnimation ballAnimation;
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
        Ball = GetComponent<Ball>();
        BallAnimation = GetComponent<BallAnimation>();
    }
}
