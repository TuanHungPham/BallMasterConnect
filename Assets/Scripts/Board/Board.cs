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
    [SerializeField] private GameObject[,] mainBoardMatrix = new GameObject[ROW, COLUM];
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
        InitializeNewBoard();
    }

    private void InitializeNewBoard()
    {
        Vector3 spawnPoint = transform.position;

        for (int i = 0; i < ROW; i++)
        {
            for (int j = 0; j < COLUM; j++)
            {
                GameObject holder = Instantiate(ballHoder);
                float postitionX = firstPos + j * spacingX;
                spawnPoint.x = postitionX;
                holder.transform.position = spawnPoint;

                holder.transform.SetParent(transform);
                holder.name = $"[{i},{j}]";

                mainBoardMatrix[i, j] = holder;
            }

            spawnPoint.y -= spacingY;
        }
    }

    private void SetMatrixPos(GameObject holder)
    {
        BallHolder ballHolder = holder.GetComponent<BallHolder>();
        if (ballHolder == null) return;

    }
}
