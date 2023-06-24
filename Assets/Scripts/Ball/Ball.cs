using UnityEngine;
using TigerForge;

public class Ball : MonoBehaviour
{
    #region public
    public bool IsSelected { get => isSelected; set => isSelected = value; }
    #endregion

    #region private
    [SerializeField] private bool isSelected;
    #endregion

    private void OnMouseDown()
    {
        IsSelected = true;

        EventManager.SetData(EventID.BALL_SELECTING.ToString(), this.gameObject);
        EventManager.EmitEvent(EventID.BALL_SELECTING.ToString());

        Debug.Log($"Clicking at {gameObject.name}");
    }

    private void OnMouseUp()
    {
        IsSelected = false;

        EventManager.EmitEvent(EventID.BALL_RELEASING.ToString());
    }

    private void OnMouseEnter()
    {
        if (!ConnectGameplayManager.Instance.IsDragging) return;

        EventManager.SetData(EventID.BALL_CONNECTING.ToString(), this.gameObject);
        EventManager.EmitEvent(EventID.BALL_CONNECTING.ToString());

        Debug.Log($"Mouse move through {gameObject.name}");
    }

    private void OnMouseExit()
    {
    }
}
