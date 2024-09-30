using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private bool move = false; //Used for controlling when the platform should move
    [SerializeField] private Transform target; //Target position the platform should move towards

    //Sets player as child of platform when stood on
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Transform character = other.transform.parent;
            character.SetParent(transform);
            move = true;
        }
    }

    //Removes parenting when player steps off platform
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Transform character = other.transform.parent;
            character.SetParent(null);
            move = false;
        }
    }

    private void Update()
    {
        if (move)
        {
            //Moves the platform towards target
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime);
            Physics.SyncTransforms();
        }
    }
}
