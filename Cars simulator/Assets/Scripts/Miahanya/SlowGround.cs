using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SlowGround : MonoBehaviour
{
    public float speed;
    [Range(0, 1)] float slowness;
    private bool isGrounded;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground") && !isGrounded)
        {
            isGrounded = true;
            speed *= slowness;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground") && isGrounded)
        {
            isGrounded = false;
            speed /= slowness;
        }
    }
}
