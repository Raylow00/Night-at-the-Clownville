using UnityEngine;

public interface IBulletDecoratable
{
    void SpawnBulletHoles(RaycastHit point, WeaponStats weaponStats);
    void SpawnBulletHoles(Collision collision, WeaponStats weaponStats);
}
