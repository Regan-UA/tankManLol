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
        // direction від гравця зазвичай приходить як (x, 0, z)
        // Ми передаємо його як локальний напрямок
        MoveObjectRb(direction.normalized, tankStats.MovementSpeed, tankStats.MovementSmoothing);

        // Для повороту використовуємо той самий напрямок
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
