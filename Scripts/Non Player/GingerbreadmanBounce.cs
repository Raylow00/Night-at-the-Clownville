using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class GingerbreadmanBounce : MonoBehaviour
{
    [Header("Bounce up force")]
    [SerializeField] private float upForce = 5f;
    [Header("Bounce timer")]
    [SerializeField] private float bounceTimer;
    [Header("Bounce particles tag")]
    [SerializeField] private string bounceParticlesTag;

    [Header("Forward force")]
    [SerializeField] private float forwardForce = 5f;

    private float explosionRadius = 10f;
    private float bounceTime;
    private Rigidbody rb;
    private GameObject player;
    private Vector3 playerDirection;
    private ObjectPooler objectPooler;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        
        bounceTime = bounceTimer;
        objectPooler = ObjectPooler.instance;
    }

    // Update is called once per frame
    void Update()
    {
        bounceTime--;
        if (bounceTime <= 0f)
        {
            // Debug.Log("[GingerbreadmanBounce] Bouncing");
            bounceTime = bounceTimer;
            rb.AddExplosionForce(upForce, transform.position, explosionRadius, upForce, ForceMode.Impulse);

            playerDirection = (player.transform.position - transform.position).normalized * forwardForce;
            rb.AddForce(playerDirection, ForceMode.Impulse);
        }
        
        transform.LookAt(player.transform);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 6)
        {
            objectPooler.SpawnFromPool(bounceParticlesTag, transform.position, Quaternion.identity);
        }

        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerHealthSystem>().TakeDamage_TEMP();
        }
    }
}
