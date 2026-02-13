using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAi : MonoBehaviour
{
    [Header("Ranges")]
    [SerializeField] private float aggroRange = 15f;
    [SerializeField] private float attackRange = 0;

    [Header("Attack")]
    [SerializeField] private int damagePerTick = 10;
    [SerializeField] private float attackCooldown = 1.2f;

    private NavMeshAgent agent;
    private Transform player;
    private float lastAttackTime;
    public Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackRange;
    }

    private void Start()
    {
        player = PlayerManager.Instance?.PlayerTransform;

        if (player == null)
            Debug.LogError("Player not found in PlayerManager!");
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);
        bool isMoving = agent.velocity.magnitude > 0.1f;
        animator.SetBool("Walking", true);


        if (distance > aggroRange)
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walking", false);
            agent.isStopped = true;
            agent.autoBraking = true;
            return;
        }

        agent.isStopped = false;
        agent.SetDestination(player.position);
        
        if (distance <= attackRange)
        {
            Debug.Log(attackRange);
            Debug.Log("player in Range");
            TryAttack();
        }
        else
            animator.SetBool("Attack1", false);
    }

    private void TryAttack()
    {
        Debug.Log("TryAttack called");
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        lastAttackTime = Time.time;

        if (player.GetComponentInParent<IDamageable>() is IDamageable damageable)
        {
           
            damageable.TakeDamage(damagePerTick);
            animator.SetBool("Attack1", true);
            Debug.Log("Enemy attacking player");
        }
    }
}
