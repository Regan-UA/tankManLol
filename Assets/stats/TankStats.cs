using UnityEngine;

[CreateAssetMenu(fileName = "TankStats", menuName = "Scriptable Objects/TankStats")]
public class TankStats : UnitMovableStats
{
    public float MovementSmoothing = 0.3f;
    public float HullRotationSpeed = 150f;
    public float HullRotationSmoothing = 0.01f;
}
