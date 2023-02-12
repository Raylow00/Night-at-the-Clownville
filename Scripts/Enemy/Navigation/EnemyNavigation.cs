using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum NavigationMode
{
    NAV_IDLE,
    NAV_PATROL,
    NAV_CHASE
}

public enum EnemyTriggerEvent
{
    ENTER_CHASE_RANGE_EVENT,
    EXIT_CHASE_RANGE_EVENT,
    IDLE_PATROL_TIMEOUT_EVENT
}

public class EnemyNavigation : MonoBehaviour
{
    #region Serialized Fields
    [Header("Time")]
    [SerializeField] private int idleModeHoldDuration;
    [SerializeField] private int patrolModeHoldDuration;

    [Header("Patrol")]
    [SerializeField] private float patrolSpeed;
    [SerializeField] private Transform[] wayPoints;

    [Header("Chase")]
    [SerializeField] private Transform chaseTarget;
    [SerializeField] private float chaseRange;
    [SerializeField] private float chaseSpeed;

    [Header("Events")]
    [SerializeField] private IntEvent onNavModeChangeEvent;
    [SerializeField] private IntEvent onChaseTargetEnterTriggerEvent;
    #endregion

    #region Private Fields
    private NavMeshAgent navMeshAgent;
    private NavigationMode currNavMode = NavigationMode.NAV_IDLE;

    private int destPoint = 0;
    private float navModeIdleCounter = 0;
    private float navModePatrolCounter = 0;
    #endregion

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        ResetNavMeshAgent();

        if (chaseTarget == null) chaseTarget = GameObject.FindWithTag("Player").transform;

        // Reset idle - patrol counter
        navModeIdleCounter = 0;
        navModePatrolCounter = 0;
    }

    void Update()
    {
        if (currNavMode == NavigationMode.NAV_IDLE)
        {
            navModePatrolCounter = 0;
            navModeIdleCounter += Time.deltaTime;
            // When idle counter reaches threshold
            // Trigger state machine to change to patrol state
            if (navModeIdleCounter > idleModeHoldDuration)
            {
                navModeIdleCounter = 0;
                ProcessStateMachine(EnemyTriggerEvent.IDLE_PATROL_TIMEOUT_EVENT);
            }
        }
        else if (currNavMode == NavigationMode.NAV_PATROL)
        {
            navModeIdleCounter = 0;
            navModePatrolCounter += Time.deltaTime;
            // When patrol counter reaches threshold
            // Trigger state machine to change to idle state
            if (navModePatrolCounter > patrolModeHoldDuration)
            {
                navModePatrolCounter = 0;
                ProcessStateMachine(EnemyTriggerEvent.IDLE_PATROL_TIMEOUT_EVENT);
            }
        }

        // If current state is Patrol, 
        // keep going to the next point
        // Otherwise, it will stop at one point only
        if (!navMeshAgent.pathPending &&
            navMeshAgent.remainingDistance < 0.5f &&
            currNavMode == NavigationMode.NAV_PATROL)
        {
            GotoNextPoint();
        }

        // Calculate distance with chase target
        if (Vector3.Distance(transform.position, chaseTarget.transform.position) <= 10)
        {
            Debug.Log("Target entered range");
            onChaseTargetEnterTriggerEvent.Raise(0);
            ProcessStateMachine(EnemyTriggerEvent.ENTER_CHASE_RANGE_EVENT);
        }
        else
        {
            Debug.Log("Target exit range");
            onChaseTargetEnterTriggerEvent.Raise(1);
            ProcessStateMachine(EnemyTriggerEvent.EXIT_CHASE_RANGE_EVENT);
        }
    }

    #region Public Methods
    /// <summary>
    ///     Get the current speed of the navmeshagent
    /// </summary>
    /// <returns>
    ///     navMeshAgent.speed
    /// </returns>
    public float GetCurrentAgentSpeed()
    {
        return navMeshAgent.speed;
    }

    /// <summary>
    ///     Get the set patrol speed
    /// </summary>
    /// <returns>
    ///     patrolSpeed
    /// </returns>
    public float GetPatrolSpeed()
    {
        return patrolSpeed;
    }

    /// <summary>
    ///     Get the set chase speed
    /// </summary>
    /// <returns>
    ///     chaseSpeed
    /// </returns>
    public float GetChaseSpeed()
    {
        return chaseSpeed;
    }

    /// <summary>
    ///     Get the set chase range
    /// </summary>
    /// <returns>
    ///     chaseRange
    /// </returns>
    public float GetChaseRange()
    {
        return chaseRange;
    }

    /// <summary>
    ///     Get the current navigation mode the navmeshagent is in
    /// </summary>
    /// <returns>
    ///     currNavMode
    /// </returns>
    public NavigationMode GetNavMode()
    {
        return currNavMode;
    }

    /// <summary>
    ///     Get all the waypoints set in the editor
    /// </summary>
    /// <returns>
    ///     wayPoints
    /// </returns>
    public Transform[] GetWaypoints()
    {
        return wayPoints;
    }

    /// <summary>
    ///     Get the chase target transform in order to get the position
    /// </summary>
    /// <returns>
    ///     chaseTarget.transform
    /// </returns>
    public Transform GetChaseTargetTransform()
    {
        return chaseTarget.transform;
    }

    /// <summary>
    ///     Process enemy navigation state machine based on current state and event input
    ///     
    /// </summary>
    /// <param name="arg_enemyTriggerEvent">
    ///     0 - Chase target enters chase range
    ///     1 - Chase target exits chase range
    /// </param>
    public void ProcessStateMachine(EnemyTriggerEvent arg_enemyTriggerEvent)
    {
        switch (currNavMode)
        {
            case NavigationMode.NAV_IDLE:
                if (arg_enemyTriggerEvent == EnemyTriggerEvent.ENTER_CHASE_RANGE_EVENT)
                {
                    currNavMode = NavigationMode.NAV_CHASE;
                    SetAgentSpeed(chaseSpeed);
                    ChaseTarget(chaseTarget);
                }
                else if (arg_enemyTriggerEvent == EnemyTriggerEvent.IDLE_PATROL_TIMEOUT_EVENT)
                {
                    currNavMode = NavigationMode.NAV_PATROL;
                    SetAgentSpeed(patrolSpeed);
                    GotoNextPoint();
                }

                break;

            case NavigationMode.NAV_PATROL:
                if (arg_enemyTriggerEvent == EnemyTriggerEvent.ENTER_CHASE_RANGE_EVENT)
                {
                    currNavMode = NavigationMode.NAV_CHASE;
                    SetAgentSpeed(chaseSpeed);
                    ChaseTarget(chaseTarget);
                }
                else if (arg_enemyTriggerEvent == EnemyTriggerEvent.IDLE_PATROL_TIMEOUT_EVENT)
                {
                    currNavMode = NavigationMode.NAV_IDLE;
                    SetAgentSpeed(0f);
                }

                break;

            case NavigationMode.NAV_CHASE:
                if (arg_enemyTriggerEvent == EnemyTriggerEvent.EXIT_CHASE_RANGE_EVENT)
                {
                    currNavMode = NavigationMode.NAV_PATROL;
                    SetAgentSpeed(patrolSpeed);
                    GotoNextPoint();
                }
                else
                {
                    if (navMeshAgent.transform.position != chaseTarget.position)
                    {
                        currNavMode = NavigationMode.NAV_CHASE;
                        SetAgentSpeed(chaseSpeed);
                        ChaseTarget(chaseTarget);
                    }
                }

                break;

            default:
                currNavMode = NavigationMode.NAV_IDLE;
                break;

        }

        onNavModeChangeEvent.Raise((int)currNavMode);
        Debug.Log("Current nav mode: " + currNavMode);
    }

    /// <summary>
    ///     Sets the agent speed
    /// </summary>
    /// <param name="arg_val"></param>
    /// <returns></returns>
    public float SetAgentSpeed(float arg_val)
    {
        Debug.Log("Setting agent speed: " + arg_val);
        navMeshAgent.speed = arg_val;
        return navMeshAgent.speed;
    }

    /// <summary>
    ///     Cycles to the next point in the waypoints list
    ///     Reset the navmeshagent if it is not on the navmesh
    ///     After reset, if still not on navmesh, set it to Idle mode and exit
    /// </summary>
    public void GotoNextPoint()
    {
        Debug.Log("Moving to next point");
        if (!navMeshAgent.isOnNavMesh)
        {
            Debug.Log("Agent is not on a valid navmesh! Resetting nav mesh agent and setting its position...");
            ResetNavMeshAgent();
            Debug.Log("Reset done");
            
            // After resetting nav mesh agent,
            // if it is still not on valid nav mesh
            // exit
            if (!navMeshAgent.isOnNavMesh)
            {
                currNavMode = NavigationMode.NAV_IDLE;
                return;
            }
        }

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            // Returns if no points have been set up
            if (wayPoints.Length == 0)
            {
                currNavMode = NavigationMode.NAV_IDLE;
                return;
            }

            // Set the agent to go to the currently selected destination.
            navMeshAgent.destination = wayPoints[destPoint].position;

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            destPoint = (destPoint + 1) % wayPoints.Length;
            navMeshAgent.SetDestination(navMeshAgent.destination);
        }
    }

    /// <summary>
    ///     Set the navmeshagent destination to the chase target
    /// </summary>
    /// <param name="arg_target"></param>
    public void ChaseTarget(Transform arg_target)
    {
        Debug.Log("Chasing target");

        navMeshAgent.destination = arg_target.position;
        navMeshAgent.SetDestination(navMeshAgent.destination);
    }
    #endregion

    #region Private Methods
    /// <summary>
    ///     Resets the navmeshagent position so it can recognize the navmesh
    /// </summary>
    private void ResetNavMeshAgent()
    {
        Vector3 warpPosition = new Vector3(0f, 1f, 0f);
        navMeshAgent.transform.position = warpPosition;
        navMeshAgent.enabled = false;
        navMeshAgent.enabled = true;
    }
    #endregion
}
