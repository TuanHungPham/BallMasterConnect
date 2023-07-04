using System.Collections.Generic;
using UnityEngine;
using TigerForge;
using DG.Tweening;

public class BallHolder : MonoBehaviour
{
    #region public
    public MatrixPos matrixPos;
    public bool IsEmpty { get => isEmpty; set => isEmpty = value; }
    #endregion

    #region private
    [SerializeField] private float refillHeight;
    [SerializeField] private float ballDropTime;
    [SerializeField] private bool isEmpty;

    [Space(20)]
    [SerializeField] private GameObject currentBallHolding;
    [SerializeField] private List<GameObject> ballList = new List<GameObject>();
    [SerializeField] private GameObject redBall;
    [SerializeField] private GameObject blueBall;
    [SerializeField] private GameObject greenBall;
    [SerializeField] private GameObject orangeBall;
    [SerializeField] private GameObject purpleBall;
    #endregion

    private void Awake()
    {
        LoadComponents();
        InitializeBallList();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        redBall = Resources.Load<GameObject>("Prefabs/RedBall");
        blueBall = Resources.Load<GameObject>("Prefabs/BlueBall");
        greenBall = Resources.Load<GameObject>("Prefabs/GreenBall");
        orangeBall = Resources.Load<GameObject>("Prefabs/OrangeBall");
        purpleBall = Resources.Load<GameObject>("Prefabs/PurpleBall");

        refillHeight = 10;
        ballDropTime = 0.02f;
        IsEmpty = true;
    }

    private void Start()
    {
        ListenEvent();
        CreateRandomBall(transform.position);
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.BALL_EXPLODED.ToString(), CheckBallInHolder);
    }

    private void InitializeBallList()
    {
        ballList.Add(redBall);
        ballList.Add(blueBall);
        ballList.Add(greenBall);
        ballList.Add(orangeBall);
        ballList.Add(purpleBall);
    }

    public void CreateRandomBall(Vector3 spawnPos, bool isRefill = false)
    {
        int index = Random.Range(0, ballList.Count);

        GameObject ball = Instantiate(ballList[index]);

        if (isRefill)
        {
            ball.transform.position = new Vector3(spawnPos.x, spawnPos.y + refillHeight, spawnPos.z);
            ball.transform.DOMoveY(spawnPos.y, ballDropTime);
        }
        else
        {
            ball.transform.position = spawnPos;
        }

        ball.transform.SetParent(transform);

        currentBallHolding = ball;

        IsEmpty = false;
    }

    public void ClearBallHolding()
    {
        GameObject ball = transform.GetChild(0).gameObject;
        Destroy(ball);
    }

    public void CheckBallInHolder()
    {
        Debug.Log("Checking ball in holder...");
        if (transform.childCount != 0)
        {
            SetCurrentBallHolding(transform.GetChild(0).gameObject);
            IsEmpty = false;
            // Debug.Log($"Holder [{matrixPos.row},{matrixPos.colum}] is holding a Ball!");
            return;
        }

        SetCurrentBallHolding(null);
        IsEmpty = true;
        // Debug.Log($"Holder [{matrixPos.row},{matrixPos.colum}] is empty!");
    }

    public GameObject GetCurrentBallHolding()
    {
        return currentBallHolding;
    }

    public void SetCurrentBallHolding(GameObject obj)
    {
        currentBallHolding = obj;
    }
}

public struct MatrixPos
{
    public int row;
    public int colum;

    public void SetMatrixPos(int row, int colum)
    {
        this.row = row;
        this.colum = colum;
        // Debug.Log($"Holder is setted as [{row},{colum}]");
    }
}
