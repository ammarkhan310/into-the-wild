using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState {
    Patrolling,
    Alerted,
    TargetVisible,
    Dead
}

public class EnemyAIStateMachine : MonoBehaviour {
    [SerializeField] private EnemyState currentState = EnemyState.Patrolling;

    [Header("Patrolling")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private int waypointIndex;
    [SerializeField] private bool patrolLoop = true;
    [SerializeField] private float closeEnoughDistance = 1f;

    [Header("Alerted")]
    [SerializeField] private float lastAlertTime = 0f;
    [SerializeField] private Vector3 lastKnownTargetPosition;

    [Header("TargetVisible")]
    [SerializeField] private Transform target;


    private Animator animator;
    private NavMeshAgent agent;

    private void Awake() {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Start() {
        currentState = EnemyState.Patrolling;

        if ((agent != null) && (waypoints.Length > 0)) {
            agent.SetDestination(waypoints[waypointIndex].position);
        }
    }

    public EnemyState GetState() {
        return currentState;
    }

    public void SetState(EnemyState newState) {
        if (currentState == newState) {
            return;
        }

        currentState = newState;
        if (newState == EnemyState.Patrolling) {
            agent.enabled = true;
            waypointIndex = 0;
            agent.SetDestination(waypoints[waypointIndex].position);
        } else if (newState == EnemyState.Alerted) {
            // investigate the last known target position
            agent.enabled = true;
            GetComponent<NavMeshAgent>().acceleration = 5000f;
            GetComponent<NavMeshAgent>().speed = 3;
            agent.SetDestination(lastKnownTargetPosition);

            // remember when we were alerted
            lastAlertTime = Time.time;
        } else if (newState == EnemyState.TargetVisible) {
            animator.SetFloat("Speed", 0f);

            // remember the target's last known position
            lastKnownTargetPosition = target.position;
        } else if (newState == EnemyState.Dead) {
            // disable navigation
            agent.enabled = false;
            animator.SetFloat("Speed", 0f);
            animator.SetBool("Dead", true);
        }
    }

    public void SetTarget(Transform target) {
        this.target = target;
    }

    void Update() {
        animator.SetFloat("Speed", agent.velocity.magnitude);
        if (this.GetComponent<EnemyHealth>().dead) {
            currentState = EnemyState.Dead;
            this.gameObject.GetComponent<Collider>().transform.Translate(Vector3.down*1);
        }
         if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {

                }
            }
        }

        if (currentState == EnemyState.Dead) {
            return;
        } else if (currentState == EnemyState.Patrolling) {
            Patrol();
        } else if (currentState == EnemyState.Alerted) {
            Alert();
        }
    }

    private void Patrol() {
        GetComponent<NavMeshAgent>().speed = 1.5f;
        Vector3 destination = waypoints[waypointIndex].position;
        float distanceToDestination = Vector3.Distance(agent.transform.position, destination);
        if (distanceToDestination < closeEnoughDistance) {
            // we reached the waypoint
            waypointIndex++;

            if (waypointIndex >= waypoints.Length) {
                if (patrolLoop) {
                    // start over
                    waypointIndex = 0;
                } else {
                    animator.SetFloat("Speed", 0f);
                    return;
                }
            }

            agent.SetDestination(waypoints[waypointIndex].position);
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void Alert() {
        GetComponent<NavMeshAgent>().speed = 3f;
        animator.SetTrigger("Attack");
    }

    private void Shoot() {
        //
    }

}
