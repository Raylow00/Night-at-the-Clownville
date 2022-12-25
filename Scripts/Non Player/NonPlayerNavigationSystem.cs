using UnityEngine;
using UnityEngine.AI;

public class NonPlayerNavigationSystem : MonoBehaviour
{
    [Header("To follow on start")]
    [SerializeField] private bool toFollowOnStart;

    [Header("Nav Mesh Agent Properties")]
    [SerializeField] private float navSpeed;

    private string playerTag = "Player";
    private Collider sphereCollider;
    private float distance;
    private bool isTriggered;
    private NavMeshAgent agent;
    private GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        player = GameObject.FindWithTag("Player");
        
        agent = GetComponent<NavMeshAgent>();
        agent.speed = navSpeed;
    }

    void Update()
    {
        if(toFollowOnStart)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == playerTag)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == playerTag)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == playerTag)
        {
            agent.isStopped = true;
        }
    }
}
