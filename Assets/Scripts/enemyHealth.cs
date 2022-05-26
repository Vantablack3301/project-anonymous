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
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        Debug.Log("enemy taken damage");
    }
}
