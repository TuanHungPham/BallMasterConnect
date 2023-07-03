using System.Collections;
using TigerForge;
using UnityEngine;

public abstract class BoosterFunction : MonoBehaviour
{
    #region public
    #endregion

    #region private
    [SerializeField] protected BoosterData boosterData;
    #endregion

    protected virtual void Awake()
    {
        LoadComponents();
    }

    protected virtual void Reset()
    {
        LoadComponents();
    }

    protected abstract void LoadComponents();


    protected virtual void Start()
    {
        StartCoroutine(HandleBoosterFunction());
    }

    protected abstract IEnumerator HandleBoosterFunction();

    protected virtual void SelfDestruct()
    {
        if (transform.parent == null) return;
        Destroy(transform.parent.gameObject, DelayTimeSystem.SELF_DESTRUCT_TIME);
    }

    protected virtual void EmitUsingBoosterEvent()
    {
        EventManager.SetData(EventID.BOOSTER_USING.ToString(), boosterData);
        EventManager.EmitEvent(EventID.BOOSTER_USING.ToString());
    }
}
