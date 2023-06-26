using UnityEngine;

public class BoardManager : MonoBehaviour
{
    #region public
    public Board Board { get => board; set => board = value; }
    public ExtensionBoard ExtensionBoard { get => extensionBoard; set => extensionBoard = value; }
    #endregion

    #region private
    [SerializeField] private Board board;
    [SerializeField] private ExtensionBoard extensionBoard;
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
        board = GetComponentInChildren<Board>();
        extensionBoard = GetComponentInChildren<ExtensionBoard>();
    }
}
