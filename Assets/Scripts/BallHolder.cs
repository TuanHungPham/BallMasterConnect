using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHolder : MonoBehaviour
{
    #region public
    #endregion

    #region private
    [SerializeField] private List<GameObject> ballList = new List<GameObject>();
    [SerializeField] private GameObject redBall;
    [SerializeField] private GameObject blueBall;
    [SerializeField] private GameObject greenBall;
    [SerializeField] private GameObject orangeBall;
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
    }

    private void Start()
    {
        HoldRandomBall();
    }

    private void InitializeBallList()
    {
        ballList.Add(redBall);
        ballList.Add(blueBall);
        ballList.Add(greenBall);
        ballList.Add(orangeBall);
    }

    private void HoldRandomBall()
    {
        int index = Random.Range(0, ballList.Count);
        GameObject ball = Instantiate(ballList[index]);
        ball.transform.position = transform.position;
        ball.transform.SetParent(transform);
    }
}
