using UnityEngine;
using TigerForge;
using System.Collections.Generic;
using System;

public class LineSystem : MonoBehaviour
{
    #region public
    #endregion

    #region private
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
        EventManager.StartListening(EventID.BALL_RELEASING.ToString(), ResetLine);
    }

    private void Update()
    {
        DrawConnectedLine();
    }

    private void DrawConnectedLine()
    {
        linePointArray.Clear();

        foreach (var ball in ConnectGameplayManager.Instance.GetBallConnectSystem().GetSelectedBallList())
        {
            if (linePointArray.Contains(ball.transform.position)) continue;

            linePointArray.Add(ball.transform.position);
        }

        lineRenderer.positionCount = linePointArray.Count;
        lineRenderer.SetPositions(linePointArray.ToArray());
    }

    private void ResetLine()
    {
        linePointArray.Clear();
        lineRenderer.SetPositions(linePointArray.ToArray());
    }
}
