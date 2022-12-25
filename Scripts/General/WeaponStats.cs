using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Weapon/Stats", fileName = "WeaponStats")]
public class WeaponStats : ScriptableObject
{
    [Header("Properties")]
    public bool unlocked;
    public string weaponName;
    public string weaponID;
    public bool isMeleeWeapon;
    public float range;
    public float forceRatio;
    public int weaponMinDamage;
    public int weaponMaxDamage;
    public int weaponCurrentAmmo;
    public int weaponMaxAmmo;
    public int weaponMaxBullets;
    public Sprite weaponIconSprite;
    public Sprite weaponCrosshairSprite;
    public Texture[] shootMarks;
    public Vector3 shootMarkScale;
    [Range(0f, 30f)] public float fireRate = 15f;
    public float cameraShakeIntensity;
    public float cameraShakeDuration;
}
