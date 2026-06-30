using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// IA básica com 4 estados: Idle → Patrol → Chase → Attack
/// Requer: NavMeshAgent no mesmo GameObject
/// O cenário precisa ter NavMesh baked (Window > AI > Navigation > Bake)
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public enum State { Idle, Patrol, Chase, Attack }
    public State currentState = State.Idle;

    [Header("Referências")]
    public Transform player;

    [Header("Detecção")]
    public float detectionRange = 10f;
    public float attackRange    = 2f;
    public float fieldOfView    = 120f;
    public LayerMask obstacleMask;

    [Header("Patrulha")]
    public Transform[] patrolPoints;
    public float patrolWaitTime = 2f;

    [Header("Ataque")]
    public float attackDamage   = 10f;
    public float attackCooldown = 1.5f;

    [Header("Movimento")]
    public float patrolSpeed = 2f;
    public float chaseSpeed  = 4f;

    private NavMeshAgent agent;
    private int   patrolIndex = 0;
    private float patrolTimer = 0f;
    private float attackTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            var go = GameObject.FindGameObjectWithTag("Player");
            if (go != null) player = go.transform;
            else Debug.LogWarning("[EnemyAI] Nenhum GameObject com tag 'Player' encontrado!");
        }

        currentState = (patrolPoints != null && patrolPoints.Length > 0)
            ? State.Patrol
            : State.Idle;

        SetDestinationToPatrolPoint();
    }

    void Update()
    {
        if (player == null) return;

        if (attackTimer > 0f) attackTimer -= Time.deltaTime;

        switch (currentState)
        {
            case State.Idle:   HandleIdle();   break;
            case State.Patrol: HandlePatrol(); break;
            case State.Chase:  HandleChase();  break;
            case State.Attack: HandleAttack(); break;
        }
    }

    void HandleIdle()
    {
        agent.isStopped = true;

        if (CanSeePlayer())
            TransitionTo(State.Chase);
    }

    void HandlePatrol()
    {
        agent.isStopped = false;
        agent.speed     = patrolSpeed;

        if (CanSeePlayer())
        {
            TransitionTo(State.Chase);
            return;
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolWaitTime)
            {
                patrolTimer = 0f;
                patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
                SetDestinationToPatrolPoint();
            }
        }
    }

    void HandleChase()
    {
        agent.isStopped = false;
        agent.speed     = chaseSpeed;
        agent.SetDestination(player.position);

        

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= attackRange)
            TransitionTo(State.Attack);
        else if (!CanSeePlayer())
            TransitionTo(patrolPoints is { Length: > 0 } ? State.Patrol : State.Idle);

            
    }

    void HandleAttack()
    {
        agent.isStopped = true;
        transform.LookAt(player);

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > attackRange)
        {
            TransitionTo(State.Chase);
            return;
        }

        if (attackTimer <= 0f)
        {
            PerformAttack();
            attackTimer = attackCooldown;
        }
    }

    void PerformAttack()
    {
        var playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
            playerHealth.TakeDamage(attackDamage);

        Debug.Log($"{gameObject.name} atacou o jogador por {attackDamage} de dano!");
    }

    bool CanSeePlayer()
    {
        if (player == null) return false;

        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float   dist        = Vector3.Distance(transform.position, player.position);

        if (dist > detectionRange) return false;

        float angle = Vector3.Angle(transform.forward, dirToPlayer);
        if (angle > fieldOfView / 2f) return false;

        if (Physics.Raycast(transform.position + Vector3.up * 0.5f,
                            dirToPlayer, dist, obstacleMask))
            return false;

        return true;
    }

    void TransitionTo(State newState)
    {
        currentState = newState;
        Debug.Log($"[EnemyAI] {gameObject.name} → {newState}");
    }

    void SetDestinationToPatrolPoint()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;
        agent.SetDestination(patrolPoints[patrolIndex].position);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = new Color(1f, 1f, 0f, 0.15f);
        Vector3 left  = Quaternion.Euler(0, -fieldOfView / 2f, 0) * transform.forward;
        Vector3 right = Quaternion.Euler(0,  fieldOfView / 2f, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, left  * detectionRange);
        Gizmos.DrawRay(transform.position, right * detectionRange);

        if (patrolPoints == null || patrolPoints.Length < 2) return;
        Gizmos.color = Color.cyan;
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            if (patrolPoints[i] == null) continue;
            int next = (i + 1) % patrolPoints.Length;
            if (patrolPoints[next] != null)
                Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[next].position);
        }
    }
}
