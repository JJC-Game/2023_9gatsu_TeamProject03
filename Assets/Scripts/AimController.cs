using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    public bool targetIn;

    private void Start()
    {
        targetIn = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            Debug.Log("Hit");
            targetIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Debug.Log("Out!");
            targetIn = false;
        }
    }
}
