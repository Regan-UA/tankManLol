using UnityEngine;

public class Tank : Mover
{
    public GameObject Turret;
    private TankStats tankStats;
    protected override void Awake()
    {
        base.Awake(); // Викликає Mover.Awake() і заповнює rb
        tankStats = stats as TankStats;
    }
    public void Ride(Vector3 direction)
    {
            MoveObject(transform.forward * direction.magnitude, tankStats.MovementSpeed, tankStats.MovementSmoothing);
            RotateObject(transform, direction, tankStats.HullRotationSpeed, tankStats.HullRotationSmoothing);
    }
    public void RotateTurret(Vector3 target)
    {
            RotateObject(Turret.transform, target, tankStats.TurretRotationSpeed, tankStats.TurretRotationSmoothing);
    }
}
