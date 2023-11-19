using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    const float locomotionAnimationSmoothTime = 0.1f;

    NavMeshAgent agent;
    Animator animator;
    CharacterCombat combat;

    private bool walking = false;
    private AudioSource source;
    public AudioClip footstepsClip;
    public AudioClip hitClip;

    public AudioSource secondSource;
    

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        combat.OnAttack += OnAttack;

        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);

        if (speedPercent > 0 && !walking)
        {
            walking = true;
            //source.PlayOneShot(footstepsClip);
            source.Play();
        }
        else if(speedPercent == 0)
        {
            walking = false;
            source.Stop();
        }
            
    }

    protected virtual void OnAttack()
    {
        animator.SetTrigger("attack");

        StartCoroutine(AttackSoundDelay(0.5f));
    }

    IEnumerator AttackSoundDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        secondSource.PlayOneShot(hitClip);
    }
}
