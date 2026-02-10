using UnityEngine;

public class Turret : Mover
{
    public TurretStats turretStats;
    public Transform BulletSpawnPos;
    protected override void Awake()
    {
        base.Awake(); 
    }
    public virtual void Shoot()
    {
        DefaultBullet spawnedBullet = Instantiate(turretStats.BulletPrefab, BulletSpawnPos.position, transform.rotation).GetComponent<DefaultBullet>();
        spawnedBullet.SetStats(turretStats);
    }
    public void RotateTurret(Vector3 target)
    {
        RotateObject(transform, target, turretStats.TurretRotationSpeed, turretStats.TurretRotationSmoothing);
    }
}
