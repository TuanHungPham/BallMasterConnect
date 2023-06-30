using UnityEngine;

[CreateAssetMenu(menuName = "Data/BoosterData")]
public class BoosterData : ScriptableObject
{
    [SerializeField] private string boosterName;
    [SerializeField] private BoosterID boosterID;
    [SerializeField] private BoosterType boosterType;
    [SerializeField] private GameObject boosterPrefab;

    public string BoosterName { get => boosterName; set => boosterName = value; }
    public BoosterID BoosterID { get => boosterID; set => boosterID = value; }
    public BoosterType BoosterType { get => boosterType; set => boosterType = value; }
    public GameObject BoosterPrefab { get => boosterPrefab; set => boosterPrefab = value; }
}
