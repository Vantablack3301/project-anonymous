using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCOPILOT : MonoBehaviour
{
    //GameObjects
    public GameObject playerObject;
    public GameObject attackRoot;
    public GameObject triggerVolume;
    public Animator animator;
    public CharacterController enemyController;

    //floats
    [Range(0.0f, 1.0f)]
    public float speed = 1.0f;
    public float cooldownTime = 2;
    public float StaggerTime = 2;
    public float attackTime = 2;
    public int Damage = 100;

    //bools
    public bool Staggered;
    public bool inEffect = false;
    public bool isAttacking = false;
    public bool cooldown = false;
    public bool inRange = false;
    void Start()
    {
        //auto assign the player object and attack root, the enemy controller, and the animator
        playerObject = GameObject.FindGameObjectWithTag("Player");
        attackRoot = playerObject.transform.GetChild(0).gameObject;
        enemyController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        //set all valid animator bools (Grounded, Jump, FreeFall) to false
        animator.SetBool("Grounded", true);
        animator.SetBool("Jump", false);
        animator.SetBool("FreeFall", false);

    }

    // Update is called once per frame
    void Update()
    {   
        //set the animator float "MotionSpeed" to speed
        animator.SetFloat("MotionSpeed", speed);
        //check if the trigger volume has been triggered
        if (triggerVolume.GetComponent<trigger>().isTriggered == true)
        {
            //if the player is not in range, move towards the player object defined before using enemyController
            if (inRange == false)
            {
                //turn towards the player, but lock all other axes
                transform.LookAt(playerObject.transform);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                //move forward according to the speed variable
                enemyController.Move(transform.forward * speed * Time.deltaTime);
                //move downwards at a constant rate of -9.81
                enemyController.Move(new Vector3(0, -9.81f, 0) * Time.deltaTime);
                //call the function toggleAnimations only once
                if (inEffect == false)
                {
                    toggleAnimations(true);
                    inEffect = true;
                }
            }
            //else, if the player is not in range then run the attack coroutine with the proper timings
            else if (inRange == true)
            {
                //call the function toggleAnimations only once and pass in a false value
                if (inEffect == true)
                {
                    toggleAnimations(false);
                    inEffect = false;
                }
                //if the enemy is not in cooldown, then run the attack coroutine with the proper timings
                if (cooldown == false)
                {
                    StartCoroutine(Attack());
                }
            }
        }
    }

    //if the player enters a trigger, set inRange to true  
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    //if the player exits a trigger, or if the player becomes null, set inRange to false
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    //create an IEnumerator for the attack function
    IEnumerator Attack()
    {
        //set the bool for if the enemy is in effect to true
        //inEffect = true;
        //set the bool for if the enemy is attacking to true
        isAttacking = true;
        //set the bool for if the enemy is staggered to false
        Staggered = false;
        //set the bool for if the enemy is in cooldown to false
        cooldown = false;
        //set the bool for if the enemy is staggered to false
        Staggered = false;
        //set the attack trigger in the animator
        animator.SetTrigger("Attack");
        //wait for the the animator to finish the attack animation
        yield return new WaitForSeconds(attackTime);
        //reset the attack trigger in the animator
        animator.ResetTrigger("Attack");
        //set the bool for if the enemy is attacking to false
        isAttacking = false;
        //set the bool for if the enemy is in cooldown to true
        cooldown = true;
        //set the bool for if the enemy is staggered to false
        Staggered = false;
        //wait for the cooldown time
        yield return new WaitForSeconds(cooldownTime);
        //set the bool for if the enemy is in cooldown to false
        cooldown = false;
        //set the bool for if the enemy is staggered to false
        Staggered = false;
        //set the bool for if the enemy is in effect to false
        //inEffect = false;
    }

    //create an IEnumerator for the stagger function
    public IEnumerator Stagger()
    {
        //set the bool for if the enemy is staggered to true
        Staggered = true;
        //set the bool for if the enemy is in effect to true
        //inEffect = true;
        //set the bool for if the enemy is attacking to false
        isAttacking = false;
        //set the bool for if the enemy is in cooldown to false
        cooldown = false;
        //set the bool for if the enemy is staggered to false
        Staggered = false;
        //set the animator to stagger
        animator.SetBool("Stagger", true);
        //wait for the stagger time
        yield return new WaitForSeconds(StaggerTime);
        //set the animator to not stagger
        animator.SetBool("Stagger", false);
        //set the bool for if the enemy is staggered to false
        Staggered = false;
        //set the bool for if the enemy is in cooldown to true
        cooldown = true;
        //set the bool for if the enemy is staggered to false
        Staggered = false;
        //wait for the stagger time
        yield return new WaitForSeconds(StaggerTime);
        //set the bool for if the enemy is in cooldown to false
        cooldown = false;
        //set the bool for if the enemy is staggered to false
        Staggered = false;
        //set the bool for if the enemy is in effect to false
        //inEffect = false;
    }

    //create a function called ToggleAnimations that takes in a bool named isAnimating
    public void toggleAnimations(bool isAnimating)
    {
        //if the bool isAnimating is true, then set the animator float "Speed" to speed
        if (isAnimating == true)
        {
            Debug.Log("animating");
            animator.SetFloat("Speed", speed);
        }
        //else, if the bool isAnimating is false, then set the animator float "Speed" to 0
        else
        {
            Debug.Log("not animating");
            animator.SetFloat("Speed", 0);
        }
    }
}
