using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moster : MonoBehaviour
{
    public float speed = 1f;

    private Rigidbody mosterRigidbody;

    void Start()
    {
        mosterRigidbody = GetComponent<Rigidbody>();
    }

    
   private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController PlayerController = other.GetComponent<PlayerController>();
            if (PlayerController != null)
            {
                PlayerController.Hit();
            }
        }
    }

}
