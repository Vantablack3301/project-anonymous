using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackEvent : MonoBehaviour
{
    [SerializeField]
    protected GameObject playerObject;
    [SerializeField]
    protected GameObject attackRoot;

    public bool canAttack;
    public bool inEffect = false;

    public float Damage = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (inEffect == false && canAttack == true)
        {
            StartCoroutine(Attack());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerObject = other.gameObject;
            attackRoot = playerObject.transform.GetChild(3).gameObject;
            canAttack = true;
            Debug.Log("player in range");
            //StartCoroutine(Attack());
        }
    }

    void OnTriggerExit(Collider other)
    {
        playerObject = null;
        attackRoot = null;
        canAttack = false;
        //Debug.Log("player out of range");
    }

    IEnumerator Attack()
    {
        inEffect = true;
        Debug.Log("begin attack");
        yield return new WaitForSeconds(2);
        Debug.Log("end wait");
        if (canAttack == true)
        {
            attackRoot.GetComponent<playerHealth>().takeDamage(Damage);
            Debug.Log("send attack script");
        }
        yield return new WaitForSeconds(2);
        inEffect = false;
    }
}
