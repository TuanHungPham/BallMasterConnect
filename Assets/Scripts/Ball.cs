using UnityEngine;
using TigerForge;

public class Ball : MonoBehaviour
{
    #region public
    #endregion

    #region private
    [SerializeField] private Vector3 startDragPoint;
    [SerializeField] private Vector3 endDragPoint;
    [SerializeField] private bool isSelected;
    #endregion

    private void OnMouseDown()
    {
        startDragPoint = Input.mousePosition;
        isSelected = true;

        EventManager.EmitEvent(EventID.BALL_SELECTING.ToString());

        Debug.Log($"Clicking at {gameObject.name}");
        Debug.Log($"Start dragging at {startDragPoint}");
    }

    private void OnMouseUp()
    {
        endDragPoint = Input.mousePosition;
        float distance = Vector3.Distance(startDragPoint, endDragPoint);
        isSelected = false;

        EventManager.EmitEvent(EventID.BALL_RELEASING.ToString());

        Debug.Log($"End dragging at {endDragPoint}");
        Debug.Log($"Dragging distance: {distance}");
    }

    private void OnMouseDrag()
    {

    }
}
