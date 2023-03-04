using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    MELEE,
    RANGE_RAYCAST,
    RANGE_PROJECTILE
}

[CreateAssetMenu(menuName = "Weapon", fileName = "WeaponScriptableObject")]
public class WeaponScriptableObject : ScriptableObject
{
    public string weaponName;
    public int weaponID;
    public bool isUnlocked;
    public bool isMeleeWeapon;
    public WeaponType weaponType;
    public float attackRange;
    public float forceRatio;
    public float fireRate;
    public float minDamage;
    public float maxDamage;
    public float cameraShakeIntensity;
    public float cameraShakeDuration;
    public int currentAmmoRound;
    public int maxAmmoRound;
    public int maxBullets;
    public Sprite iconSprite;
    public Sprite crosshairSprite;
    public Texture[] shootMark;
    public Vector3 shootMarkScale;
}
