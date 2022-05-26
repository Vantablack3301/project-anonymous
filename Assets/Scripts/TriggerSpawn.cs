using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawn : MonoBehaviour
{
    private bool canSpawn = true;
    //this whole script would ideally free up memory by only
    //spawning enemies when needed instead of keeping them active
    //and animated 100% of the time
    public GameObject triggerVolume;

    public GameObject enemyToSpawn;
    public Transform SpawnLocation;

    private GameObject PlayerThing;

    // Update is called once per frame
    void Update()
    {
        var triggerScript = triggerVolume.GetComponent<trigger>();
        if (triggerScript.isTriggered == true && canSpawn == true)
        {
            var enemyInstance = Instantiate(enemyToSpawn, SpawnLocation, SpawnLocation);
            //enemyInstance.transform.position = SpawnLocation;
            enemyInstance.GetComponent<Enemy>().TriggerOnSpawn(PlayerThing, triggerVolume);
            canSpawn = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerThing = other.gameObject;
        }
    }
}
