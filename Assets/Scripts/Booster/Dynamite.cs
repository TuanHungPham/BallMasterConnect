using UnityEngine;
using DG.Tweening;
using TigerForge;
using System.Collections;

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
        StartCoroutine(HandleBoosterFunction());
    }

    IEnumerator HandleBoosterFunction()
    {
        BoosterUsingHandler handler = BoosterManager.Instance.GetBoosterUsingHandler();
        if (!handler.IsUsingBooster()) yield break;

        MoveToTargetPoint();

        yield return new WaitForSeconds(DelayTimeSystem.ROCKET_BOOST_FUNCTION_DELAY);

        SetBoosterFunction(handler.GetUsingMatrixPos().row, handler.GetUsingMatrixPos().colum);
    }

    private void MoveToTargetPoint()
    {
        Vector3 targetPoint = BoosterManager.Instance.GetBoosterUsingHandler().GetUsingPoint();
        transform.DOMove(targetPoint, moveTime);

        EmitUsingBoosterEvent();

        SelfDestruct();
    }

    private void SetBoosterFunction(int row, int colum)
    {
        BallExplosionSystem ballExplosionSystem = ConnectGameplayManager.Instance.GetBallExplosionSystem();

        ballExplosionSystem.ExplodeAroundPosition(row, colum);
    }

    private void SelfDestruct()
    {
        Destroy(transform.parent.gameObject, DelayTimeSystem.SELF_DESTRUCT_TIME);
    }

    private void EmitUsingBoosterEvent()
    {
        EventManager.EmitEvent(EventID.BOOSTER_USING.ToString());
    }

}
