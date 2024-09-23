using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Animator>().enabled = false;
            GameObject.FindGameObjectWithTag("playerCam").SetActive(false);
        }
    }
}
