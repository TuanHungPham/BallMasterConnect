using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    #region public
    #endregion

    #region private
    private const int ROW = 6;
    private const int COLUM = 6;
    [SerializeField] private float firstPos;
    [SerializeField] private float spacingX;
    [SerializeField] private float spacingY;
    [SerializeField] private GameObject ballHoder;
    [SerializeField] private List<List<GameObject>> row = new List<List<GameObject>>();
    [SerializeField] private List<GameObject> colum;
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
        ballHoder = Resources.Load<GameObject>("Prefabs/BallHolder");

        firstPos = transform.position.x;
        spacingX = 1.5f;
        spacingY = 1.5f;
    }

    private void Start()
    {
        InitializeBoard();
    }

    private void GetBallHolder(Vector3 spawnPoint, int index)
    {
        for (int i = 0; i < COLUM; i++)
        {
            GameObject holder = Instantiate(ballHoder);
            float postitionX = firstPos + i * spacingX;
            spawnPoint.x = postitionX;
            holder.transform.position = spawnPoint;

            holder.transform.SetParent(transform);
            holder.name = $"[{index},{i}]";
            colum.Add(holder);
        }
    }

    private void InitializeBoard()
    {
        Vector3 spawPoint = transform.position;

        for (int i = 0; i < ROW; i++)
        {
            colum = new List<GameObject>();

            GetBallHolder(spawPoint, i);
            row.Add(colum);

            spawPoint.y -= spacingY;
        }
    }
}
