using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{
    private Vector3 moveDir;
    [SerializeField]
    private float speed = 1;
    private Rigidbody rb;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        moveDir = Vector3.forward;
    }

    
    void FixedUpdate()
    {
        rb.AddForce(moveDir * speed, ForceMode.VelocityChange);
    }
}
