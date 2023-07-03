using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Dynamite : BoosterFunction
{
    #region public
    #endregion

    #region private
    [SerializeField] private float moveTime;
    private MatrixPos targetPos;
    #endregion

    protected override void LoadComponents()
    {
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override IEnumerator HandleBoosterFunction()
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

    protected override void SelfDestruct()
    {
        base.SelfDestruct();
    }

    protected override void EmitUsingBoosterEvent()
    {
        base.EmitUsingBoosterEvent();
    }
}
