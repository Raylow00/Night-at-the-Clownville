using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private Rigidbody projectileRB;
    [SerializeField] private WeaponStats weaponStats;
    [SerializeField] private float speed = 25f;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask interactableLayer;

    [Header("Overlap Trigger Object")]
    [SerializeField] private GameObject sphereOverlap;

    [Header("Explosion VFX")]
    [SerializeField] private GameObject[] rocketExplosions;
    [SerializeField] private ParticleSystem hitParticles;
    
    [Header("Projectile Mesh")]
    [SerializeField] private MeshRenderer projectileMesh;
    
    [Header("Audio Manager")]
    [SerializeField] private AudioSource projectileFlightAudioSource;

    private bool isTargetHit;
    private Vector3 dir;

    void Start()
    {
        dir = (Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0)).GetPoint(1000) - transform.position + new Vector3(0f, 1f, 0f)).normalized;
    }

    void Update()
    {
        if(isTargetHit) return;
        
        transform.position += dir.normalized * (speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!enabled) return;

        // Debug.Log(collision.gameObject.name);

        Explode(collision);
        projectileMesh.enabled = false;
        isTargetHit = true;

        projectileFlightAudioSource.Stop();

        foreach(Collider col in GetComponents<Collider>())
        {
            col.enabled = false;
        }

        // Check if sphere is overlapped or collided
        Collider[] overlappedColliders = Physics.OverlapSphere(sphereOverlap.transform.position, 1f);
        foreach(var overlappedCollider in overlappedColliders)
        {
            if(overlappedCollider.gameObject.GetComponent<IDamageable>() != null)
            {
                // Debug.Log("Indirect hit - overlap");
                overlappedCollider.gameObject.GetComponent<IDamageable>().TakeDamage(damage, collision.GetContact(0).point, collision.GetContact(0).normal);
            }
        }

        if(collision.gameObject.GetComponent<IDamageable>() != null)
        {
            // Debug.Log("Direct hit");
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(damage, collision.GetContact(0).point, collision.GetContact(0).normal);
        }

        if(collision.gameObject.GetComponent<IBulletDecoratable>() != null)
        {
            collision.gameObject.GetComponent<IBulletDecoratable>().SpawnBulletHoles(collision, weaponStats);
        }

        if(collision.gameObject.GetComponent<IBreakable>() != null)
        {                    
            collision.gameObject.GetComponent<IBreakable>().BreakObject();
        }

        hitParticles.Stop();

        Destroy(gameObject, 5f);
    }

    private void Explode(Collision col)
    {
        foreach(GameObject explosion in rocketExplosions)
        {
            GameObject newExplosion = Instantiate(explosion, transform.position, explosion.transform.rotation, null);
        }
    }
}
