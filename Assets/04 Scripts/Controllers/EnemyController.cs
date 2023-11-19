using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    const float locomotionAnimationSmoothTime = 0.1f;

    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;
    Animator animator;
    EnemyStats myStats;

    AudioSource source;
    public AudioClip enemyHitClip;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        myStats = GetComponent<EnemyStats>();
        animator = GetComponentInChildren<Animator>();

        source = GetComponent<AudioSource>();

        combat.OnAttack += OnAttack;
    }

    // Update is called once per frame
    void Update()
    {
        if (myStats.dead)
            return;

        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);

        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if(distance <= agent.stoppingDistance)
            {
                // attack
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if(targetStats != null)
                {
                    combat.Attack(targetStats);
                }

                // face target
                FaceTarget();
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    protected virtual void OnAttack()
    {
        animator.SetTrigger("attack");

        StartCoroutine(AttackSoundDelay(0.5f));
    }

    public virtual void OnDeath()
    {
        animator.SetTrigger("dead");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    IEnumerator AttackSoundDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        source.PlayOneShot(enemyHitClip);
    }
}
