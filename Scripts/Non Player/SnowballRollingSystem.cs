using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballRollingSystem : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody rb;
    private Transform playerTransform;
    private Vector3 rollDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        rollDirection = (playerTransform.position - transform.position).normalized;
        rb.AddForce(rollDirection * speed, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerHealthSystem>().TakeDamage_TEMP();
        }
    }
}
