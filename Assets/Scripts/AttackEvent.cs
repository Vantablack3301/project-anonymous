using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using System.Linq;

public class AttackEvent : MonoBehaviour
{
    public float cooldownTime = 2;
    public float Damage = 100;

    protected bool cooldown = false;
    public GameObject playerObject;
    List<GameObject> AttackRange = new List<GameObject>();
    // Start is called before the first frame update

    void Update()
    {
        StartCoroutine(Cooldown());
        //var playerController = playerObject.GetComponent<ThirdPersonController>();
        var playerScript = playerObject.GetComponent(typeof(ThirdPersonController)) as ThirdPersonController;
        float isAttacking = Input.GetAxis("Fire1");
        if (isAttacking > .1 && cooldown == false)
        {
            cooldown = true;
            playerScript.canMove = false;
            //Debug.Log("cooldown in effect");
            foreach (GameObject enemy in AttackRange.ToList())
            {
                if (enemy == null)
                {
                    AttackRange.Remove(enemy);
                }
                else
                {
                    enemy.GetComponent<enemyHealth>().TakeDamage(Damage);
                }

            }
        }
        /*if (cooldown == true)
        {
            yield return new WaitForSeconds(cooldownTime);
            cooldown = false;
        }*/
    }

    IEnumerator Cooldown()
    {
        if (cooldown == true)
        {
            var playerScript = playerObject.GetComponent(typeof(ThirdPersonController)) as ThirdPersonController;
            /*while(playerScript.isGrounded == false){
                yield return null;
            }*/
            yield return new WaitForSeconds(cooldownTime);
            cooldown = false;
            playerScript.canMove = true;
            //Debug.Log("cooldown ended");
        }
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(AttackRange);
        if (other.CompareTag("Enemy"))
        {
            if (AttackRange.Contains(other.gameObject))
            {
                Debug.Log("item already in list, wont add");
            }
            else
            {
                AttackRange.Add(other.gameObject);
            }
            //Debug.Log("added " + other.gameObject + " to Attack Range");
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log(AttackRange);
        if (other.CompareTag("Enemy"))
        {
            AttackRange.Remove(other.gameObject);
            //Debug.Log("removed " + other.gameObject + " from Attack Range");
        }
    }

}
