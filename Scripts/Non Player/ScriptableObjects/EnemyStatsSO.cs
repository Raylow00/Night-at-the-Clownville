using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Stats", fileName = "EnemyStats")]
public class EnemyStatsSO : ScriptableObject
{
    public string enemyName;

    [Header("Health")]
    public int enemyMaxHealth;
    public int enemyCurrentHealth;
}
