using System;
using TigerForge;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    #region public
    #endregion

    #region private
    private const float DELAY_FLY = 0.2f;
    private const float SELF_DESTRUCT_TIME = 1f;
    [SerializeField] private float flySpeed;

    [Space(20)]
    [SerializeField] private Rigidbody2D rb2d;
    private string objName;
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
        rb2d = GetComponent<Rigidbody2D>();

        objName = gameObject.name;
    }

    private void Start()
    {
        HandleBoosterFunction();
        Invoke(nameof(Fly), DELAY_FLY);
        EmitUsingBoosterEvent();
    }

    private void HandleBoosterFunction()
    {
        BoosterUsingHandler handler = BoosterManager.Instance.GetBoosterUsingHandler();
        if (!handler.IsUsingBooster()) return;

        SetPlacementPosition(handler);

        transform.parent.position = handler.GetUsingPoint();

        SetRocketBoosterFunction(handler.GetUsingMatrixPos().row, handler.GetUsingMatrixPos().colum);
    }

    private void SetPlacementPosition(BoosterUsingHandler handler)
    {
        transform.parent.position = handler.GetUsingPoint();
    }

    private void SetRocketBoosterFunction(int row, int colum)
    {
        BallExplosionSystem ballExplosionSystem = ConnectGameplayManager.Instance.GetBallExplosionSystem();

        ballExplosionSystem.ExplodeAllBallInRow(row);
        ballExplosionSystem.ExplodeAllBallInColum(colum);
    }

    private void Fly()
    {
        Vector2 flyDirection = new Vector2();
        switch (objName)
        {
            case "Rocket1":
                flyDirection = Vector2.left;
                break;
            case "Rocket2":
                flyDirection = Vector2.up;
                break;
            case "Rocket3":
                flyDirection = Vector2.right;
                break;
            case "Rocket4":
                flyDirection = Vector2.down;
                break;
            default:
                break;
        }

        rb2d.velocity = flySpeed * flyDirection;

        SelfDestruct();
    }

    private void SelfDestruct()
    {
        if (transform.parent == null) return;
        Destroy(transform.parent.gameObject, SELF_DESTRUCT_TIME);
    }

    private void EmitUsingBoosterEvent()
    {
        EventManager.EmitEvent(EventID.BOOSTER_USING.ToString());
    }
}
