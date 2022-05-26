using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBACKUP : MonoBehaviour
{
    // Adjust the speed for the application.
    public float speed = 1.0f;
    public float cooldownTime = 2;
    public float StaggerTime = 2;
    public float attackTime = 2;
    public int Damage = 100;

    //protected float newAnimatorSpeed = Mathf.MoveTowards(prevAnimatorSpeed, targetSpeed, floatTransitionSpeed * Time.deltaTime);

    public GameObject triggerVolume;
    [SerializeField]
    protected GameObject playerObject;
    [SerializeField]
    protected GameObject attackRoot;
    [SerializeField]
    protected enemyAttackEvent selfAttackScript;
    //protected GameObject attackRoot = playerObject.transform.GetChild(3).gameObject;

    // The target (cylinder) position.
    [SerializeField]
    private Transform target;
    [SerializeField]
    private bool canAttack = false;
    [SerializeField]
    protected bool isAttacking = false;
    [SerializeField]
    private bool Staggered;
    [SerializeField]
    private bool cooldown;
    [SerializeField]
    protected float Step;

    private bool inEffect;
    [SerializeField]
    private Animator animator;
    //private float Step;
    //private bool isAnimating = false;

    //SphereCollider attackRange = GetComponent<SphereCollider>();

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Grounded", true);
        animator.SetBool("Jump", false);
        animator.SetBool("FreeFall", false);
    }

    void FixedUpdate()
    {
        inEffect = selfAttackScript.inEffect;
        Rigidbody rb = GetComponent<Rigidbody>();
        animator.SetFloat("MotionSpeed", 1);
    }


    void Update()
    {
        var triggerScript = triggerVolume.GetComponent<trigger>();
        if (triggerScript.isTriggered == true)
        {
            // Move our position a Step closer to the target.
            if (canAttack == false && /*inEffect == false &&*/ cooldown == false)
            {
                Step = speed * Time.deltaTime; // calculate distance to move
                //animator.SetFloat("Speed", speed);
                
                toggleAnimations(true);
                transform.position = Vector3.MoveTowards(transform.position, target.position, Step);
            }
            else
            {
                Step = 0f;//speed * Time.deltaTime; // calculate distance to move
                //animator.SetFloat("Speed", Step);
                toggleAnimations(false);
                transform.position = Vector3.MoveTowards(transform.position, target.position, Step);
                //Attack();
            }



            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, target.position) < 0.001f)
            {
                // Swap the position of the cylinder.
                target.position *= -1.0f;
            }
        }
    }

    /*private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerObject = other.gameObject;
            attackRoot = playerObject.transform.GetChild(3).gameObject;
            canAttack = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerObject = null;
            attackRoot = null;
            canAttack = false;
        }
    }

    /*IEnumerator Attack(float inTime)
    {
        if (canAttack == true)
        {
            yield return new WaitForSeconds(inTime);
            isAttacking = false;
            attackRoot.GetComponent<playerHealth>().takeDamage(Damage);
            Debug.Log("cooldown ended");
        }
    }*/

    IEnumerator Cooldown(float inTime)
    {
        if (cooldown == true)
        {
            yield return new WaitForSeconds(inTime);
            isAttacking = false;
            cooldown = false;
            //Debug.Log("cooldown ended");
        }
    }

    /*IEnumerator Stagger(float inTime)
    {
        if (Staggered == true)
        {
            yield return new WaitForSeconds(inTime);
            Staggered = false;
            //Debug.Log("enemy not staggered anymore");
        }
    }*/
    public void toggleAnimations (bool isAnimating) {
        //isAnimating = con;
        if (isAnimating == true)
        {
            animator.SetFloat("Speed", speed);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
    }
}

