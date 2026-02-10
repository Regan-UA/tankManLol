using UnityEngine;

public class Tank : Mover
{
    public Turret Turret;
    public TankStats tankStats;
    protected override void Awake()
    {
        base.Awake(); // Викликає Mover.Awake() і заповнює rb
    }
    public void Ride(Vector3 direction)
    {
            MoveObjectRb(transform.forward * direction.magnitude, tankStats.MovementSpeed, tankStats.MovementSmoothing);
            RotateObject(transform, direction, tankStats.HullRotationSpeed, tankStats.HullRotationSmoothing);
    }
    public void RotateTurret(Vector3 target)
    {
         Turret.RotateTurret(target);
    }
    protected virtual void Shoot()
    {
        Turret.Shoot();
    }
}
