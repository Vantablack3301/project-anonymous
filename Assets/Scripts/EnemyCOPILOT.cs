using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCOPILOT : MonoBehaviour
{
    //initialize variables for the players gameonbject, the attackroot, and the trigger volume
    public GameObject playerObject;
    public GameObject attackRoot;
    public GameObject triggerVolume;
    //get this objects own character controller and assign it to a variable
    public CharacterController enemyController;
    //initialize variables for the speed, cooldown time, stagger time, and attack time
    public float speed = 1.0f;
    public float cooldownTime = 2;
    public float StaggerTime = 2;
    public float attackTime = 2;
    //initialize variables for the damage, and the bool for if the enemy is staggered
    public int Damage = 100;
    public bool Staggered;
    //initialize variables for the bool for if the enemy is in effect, and the bool for if the enemy is attacking
    public bool inEffect = false;
    public bool isAttacking = false;
    //initialize variables for the bool for if the enemy is in cooldown, and the bool for if the enemy is staggered
    public bool cooldown = false;
    //initialize variables for the animator
    public Animator animator;
    //initialize bool for if the player is in range
    public bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        //auto assign the player object and attack root, and avoid doing anything to the trigger volume
        playerObject = GameObject.FindGameObjectWithTag("Player");
        attackRoot = playerObject.transform.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //check if the trigger volume has been triggered
        if (triggerVolume.GetComponent<TriggerVolume>().triggered == true)
        {
            //if the player is not in range, move towards the player object defined before using enemyController
            if (inRange == false)
            {
                //get the players current location
                Vector3 playerLocation = playerObject.transform.position;
                //move towards playerLocation
                enemyController.Move(playerLocation * Time.deltaTime);
            }
            //else, if the player is not in range then run the attack coroutine with the proper timings
            else if (inRange == true)
            {
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
        inEffect = true;
        //set the bool for if the enemy is attacking to true
        isAttacking = true;
        //set the bool for if the enemy is staggered to false
        Staggered = false;
        //set the bool for if the enemy is in cooldown to false
        cooldown = false;
        //set the bool for if the enemy is staggered to false
        Staggered = false;
        //set the animator to attack
        animator.SetBool("Attack", true);
        //wait for the attack time
        yield return new WaitForSeconds(attackTime);
        //set the animator to not attack
        animator.SetBool("Attack", false);
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
        inEffect = false;
    }

    //create an IEnumerator for the stagger function
    public IEnumerator Stagger()
    {
        //set the bool for if the enemy is staggered to true
        Staggered = true;
        //set the bool for if the enemy is in effect to true
        inEffect = true;
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
        //wait for the cooldown time
        yield return new WaitForSeconds(cooldownTime);
        //set the bool for if the enemy is in cooldown to false
        cooldown = false;
        //set the bool for if the enemy is staggered to false
        Staggered = false;
        //set the bool for if the enemy is in effect to false
        inEffect = false;
    }

}
