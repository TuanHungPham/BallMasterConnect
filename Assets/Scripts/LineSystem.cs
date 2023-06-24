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
    private bool isDragging;
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


    private void Update()
    {
        // DrawDraggingLine();
    }

    private void StartDrawLine()
    {
        EventManager.GetData(EventID.BALL_SELECTING.ToString());
        startDragPoint = Input.mousePosition;
        linePointArray.Add(startDragPoint);
        lineRenderer.SetPositions(linePointArray.ToArray());
        isDragging = true;
    }

    private void DrawDraggingLine()
    {
        if (!isDragging) return;

        Vector3 currentMousePos = Input.mousePosition;

        linePointArray.Add(currentMousePos);
    }

    private void ResetLine()
    {
        linePointArray.Clear();
        lineRenderer.SetPositions(linePointArray.ToArray());
    }
}
