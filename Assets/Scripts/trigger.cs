using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    //[HideInInspector] 
    public bool isTriggered = false;

    public GameObject PlayerThing;

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTriggered = true;
            PlayerThing = other.gameObject;
        }
    }
}
