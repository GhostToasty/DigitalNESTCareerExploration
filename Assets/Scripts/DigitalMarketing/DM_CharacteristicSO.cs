using UnityEngine;

[CreateAssetMenu()]
public class DM_CharacteristicSO : ScriptableObject
{
    //info that's attached to each characteristic 
    // public Transform prefab;
    public Sprite sprite;
    public string characteristicName;
    public string characteristicDescription;
    public string characteristicQuestion1;
    public string characteristicQuestion2;
    public string characteristicQuestion3;
}
