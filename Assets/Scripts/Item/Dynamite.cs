using UnityEngine;
using DG.Tweening;

public class Dynamite : MonoBehaviour
{
    #region public
    #endregion

    #region private
    [SerializeField] private float moveTime;
    #endregion

    private void Start()
    {
        MoveToTargetPoint();
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
