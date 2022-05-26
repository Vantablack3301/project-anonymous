using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class playerHealth : MonoBehaviour
{
    public float health = 300;

    public float staggerDamage = 80;
    public float staggerTime = 1;

    public GameObject playerObject;
    public ThirdPersonController playerScript;
    
    public void takeDamage(float damage) 
    {
        health -= damage;
        Debug.Log("player has taken damage, " + health + " health remaining");
        if (damage >= staggerDamage)
        {
            StartCoroutine(Stagger());
        }
    }

    IEnumerator Stagger()
    {
        playerScript.canMove = false;
        yield return new WaitForSeconds(staggerTime);
        playerScript.canMove = true;
    }
}
