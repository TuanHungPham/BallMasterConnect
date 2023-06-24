using UnityEngine;
using TigerForge;
using System.Collections.Generic;
using System;

public class LineSystem : MonoBehaviour
{
    #region public
    #endregion

    #region private
    [SerializeField] private Vector3 startDragPoint;
    [SerializeField] private Vector3 endDragPoint;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private List<Vector3> linePointArray;
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
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.BALL_SELECTING.ToString(), StartDrawLine);
        EventManager.StartListening(EventID.BALL_RELEASING.ToString(), ResetLine);
    }

    private void StartDrawLine()
    {
        startDragPoint = Input.mousePosition;
        linePointArray.Add(startDragPoint);

        lineRenderer.SetPositions(linePointArray.ToArray());
    }

    private void DrawConnectedLine()
    {
        Vector3 currentMousePos = Input.mousePosition;

        linePointArray.Add(currentMousePos);
    }

    private void ResetLine()
    {
        linePointArray.Clear();
        lineRenderer.SetPositions(linePointArray.ToArray());
    }
}
