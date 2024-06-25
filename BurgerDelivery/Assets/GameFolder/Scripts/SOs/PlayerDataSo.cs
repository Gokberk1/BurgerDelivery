using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData", menuName ="ScriptableObjects/Data")]
public class PlayerDataSo : ScriptableObject
{
    public int MaxLoad => _maxLoad;
    [SerializeField] private int _maxLoad;
}
