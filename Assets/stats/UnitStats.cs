using UnityEngine;

[CreateAssetMenu(fileName = "UnitStats", menuName = "Scriptable Objects/UnitStats")]
public class UnitStats : ScriptableObject
{
    public float MaxHealth;
    internal float curHealth;
    public float HealthRegeneration;
}
