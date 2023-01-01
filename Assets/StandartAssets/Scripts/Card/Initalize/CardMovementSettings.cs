using UnityEngine;

[CreateAssetMenu(menuName = "CardMovementSettings",fileName = "CardMovementSettings")]
public class CardMovementSettings : ScriptableObject
{
    [SerializeField] private float _cardSpeed;
    public float CardSpeed { get { return  _cardSpeed;} }
    
    [SerializeField] private float lerpTime;
    public float LerpTime { get { return lerpTime;} }
}
