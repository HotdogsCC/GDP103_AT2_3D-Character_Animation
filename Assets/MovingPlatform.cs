using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private bool move = false;
    [SerializeField] private Transform target;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Transform character = other.transform.parent;
            character.SetParent(transform);
            move = true;
        }
    }

    private void Update()
    {
        if (move)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime);
            Physics.SyncTransforms();
        }
    }
}
