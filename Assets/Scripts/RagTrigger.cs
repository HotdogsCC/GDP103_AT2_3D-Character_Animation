using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagTrigger : MonoBehaviour
{
    //Runs when player enter rag doll zone
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Disabling the animator component allows the rag doll physics to work
            other.GetComponent<Animator>().enabled = false;

            //By disabling the main camera, Cinemachine will automatically use the Rag Doll camera
            GameObject.FindGameObjectWithTag("playerCam").SetActive(false);
        }
    }
}
