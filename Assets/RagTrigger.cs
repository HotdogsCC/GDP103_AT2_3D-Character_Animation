using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagTrigger : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hi");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("yo");
    }
}
