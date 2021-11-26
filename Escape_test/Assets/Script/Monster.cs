using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 1f;

    private Rigidbody mosterRigidbody;

    void Start()
    {
        mosterRigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }
    /*
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
    */
}
