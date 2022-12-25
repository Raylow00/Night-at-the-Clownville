using UnityEngine;
public interface IDamageable
{
    void TakeDamage(int damage, Vector3 hitPoint, Vector3 hitNormal, bool isMeleeWeapon=false, bool toBroadcast=true);
}
