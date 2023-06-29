using UnityEngine;
using DG.Tweening;

public class Dynamite : MonoBehaviour
{
    #region public
    #endregion

    #region private
    [SerializeField] private float moveTime;
    private MatrixPos targetPos;
    #endregion

    private void Start()
    {
    }

    private void Update()
    {
    }

    private void MoveToTargetPoint()
    {
        Vector3 targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPoint.z = 0;
        transform.DOMove(targetPoint, moveTime);
    }
}
