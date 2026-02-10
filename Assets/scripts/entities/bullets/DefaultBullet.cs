using UnityEngine;

public class DefaultBullet : Mover
{
    [HideInInspector] private TurretStats turretStats;
    public void SetStats(TurretStats originStats)
    {
        turretStats = originStats;
    }
    private void Update()
    {
        MoveObject(transform.forward, turretStats.BulletSpeed);
    }
}
