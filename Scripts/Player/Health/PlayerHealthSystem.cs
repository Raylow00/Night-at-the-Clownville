using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO playerStats;
    [SerializeField] private FloatEvent onHealthChangeEvent;
    [SerializeField] private VoidEvent onHealthZeroEvent;
    private Health playerHealth;

    void Awake()
    {
        playerHealth = new Health(playerStats.playerCurrentHealth, playerStats.playerMaxHealth, onHealthChangeEvent, onHealthZeroEvent);
    }

    void Start()
    {
        if(onHealthChangeEvent != null) onHealthChangeEvent.Raise(playerHealth.currentHealth);
    }

    ///
    /// Temporary health damage function by colliding with enemy
    ///
    public void OnControllerColliderHit(ControllerColliderHit col)
    {
        if(col.gameObject.tag == "GiantSnowball" || col.gameObject.tag == "Gingerbreadman" || col.gameObject.layer == 7)
        {
            // Debug.Log("Touching enemy: " + col.gameObject.tag);

            playerHealth.TakeDamage(1, Vector3.zero, Vector3.zero);

            playerStats.playerCurrentHealth = playerHealth.currentHealth;
        }
    }

    public void TakeDamage_TEMP()
    {
        playerHealth.TakeDamage(1, Vector3.zero, Vector3.zero);

        playerStats.playerCurrentHealth = playerHealth.currentHealth;
    }
}
