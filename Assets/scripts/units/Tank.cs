using UnityEngine;

public class Tank : Mover
{
    public GameObject Turret;
    public float MovementSpeed = 5f;
    public float HullRotationSpeed = 150f;
    public float TurretRotationSpeed = 150f;
    public float hullMovementSmoothing = 0.3f;
    public float hullRotationSmoothing = 0.01f;
    public float turretRotationSmoothing = 0.01f;
    public void Ride(Vector3 direction)
    {
        MoveObject(transform.forward * direction.magnitude, MovementSpeed, hullMovementSmoothing);
        RotateObject(transform, direction, HullRotationSpeed, hullRotationSmoothing);
    }
    public void RotateTurret(Vector3 target)
    {      
        RotateObject(Turret.transform, target, TurretRotationSpeed, turretRotationSmoothing);
    }
}
