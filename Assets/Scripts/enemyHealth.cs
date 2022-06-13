using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public float Health = 200;
    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Debug.Log("enemy is dead");
            //wait for a few seconds
            StartCoroutine(WaitForTime(4));
            //before destroying the enemy, have its opacity fade for a few seconds
            var enemy = GetComponent<SpriteRenderer>();
            enemy.color = new Color(1, 1, 1, 0);

            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        Debug.Log("enemy taken damage");
    }

    //create a coroutine that waits for a specified amount of time
    IEnumerator WaitForTime(float seconds)
    {
        //wait for the cooldown time
        yield return new WaitForSeconds(seconds);
    }
}
