using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class playerHealth : MonoBehaviour
{
    public float health = 300;

    public float staggerDamage = 80;
    public float staggerTime = 1;
    public float animTime = 4;

    protected Animator animationObject;

    public GameObject playerObject;
    public ThirdPersonController playerScript;

    void Start()
    {
        animationObject = playerObject.GetComponent(typeof(Animator)) as Animator;
    }
    
    void Update()
    {
        if (health <= 0f)
        {
            PlayerDeath();
        }
    }

    public void takeDamage(float damage) 
    {
        health -= damage;
        Debug.Log("player has taken damage, " + health + " health remaining");
        if (damage >= staggerDamage)
        {
            StartCoroutine(Stagger());
        }
    }

    public void PlayerDeath () 
    {
        playerScript.canMove = false;   
        animationObject.SetTrigger("Death");
        StartCoroutine(DeathAnim());
    }

    IEnumerator DeathAnim() 
    {
        yield return new WaitForSeconds(animTime);
        Time.timeScale = 0f;
    }

    IEnumerator Stagger()
    {
        playerScript.canMove = false;
        yield return new WaitForSeconds(staggerTime);
        playerScript.canMove = true;
    }
}
