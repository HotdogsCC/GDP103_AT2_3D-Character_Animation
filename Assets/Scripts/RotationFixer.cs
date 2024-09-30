using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFixer : MonoBehaviour
{

    // The jump animation is not uniform, and constant jumping makes the player rotate incorrectly.
    // This is not a good solution, but it works.

    void Update()
    {
        transform.localEulerAngles = Vector3.zero;
    }
}
