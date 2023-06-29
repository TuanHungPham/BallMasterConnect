using UnityEngine;

[CreateAssetMenu(menuName = "Data/BoosterData")]
public class BoosterData : ScriptableObject
{
    [SerializeField] private string boosterName;
    [SerializeField] private BoosterID boosterID;

    public string BoosterName { get => boosterName; set => boosterName = value; }
    public BoosterID BoosterID { get => boosterID; set => boosterID = value; }
}
