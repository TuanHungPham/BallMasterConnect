using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private static BoardManager instance;
    public static BoardManager Instance { get => instance; set => instance = value; }

    #region public
    #endregion

    #region private
    [SerializeField] private Board board;
    #endregion

    private void Awake()
    {
        HandleInstanceObj();
        LoadComponents();
    }

    private void HandleInstanceObj()
    {
        instance = this;
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        board = GetComponentInChildren<Board>();
    }

    public Board GetBoardInstance()
    {
        return board;
    }
}
