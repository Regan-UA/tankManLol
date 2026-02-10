using UnityEngine;

[CreateAssetMenu(fileName = "TurretStats", menuName = "Scriptable Objects/TurretStats")]
public class TurretStats : UnitStats
{
    public float TurretRotationSpeed = 300f;
    public float TurretRotationSmoothing = 0.01f;

    public GameObject BulletPrefab;
    public float BulletDamage;
    public float ShootingSpeed;
    public float BulletSpeed = 20f;
}
