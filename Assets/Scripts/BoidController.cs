using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{
    private Vector3 moveTarget;
    [SerializeField]
    private float moveForce = .5f;
    [SerializeField]
    private float maxSpeed = 3;
    [SerializeField]
    private float moveVar = 0.1f;
    [SerializeField]
    private float targetlength = 1.5f;
    private Rigidbody rb;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (Vector3.Distance(transform.position, moveTarget) < 0.2f)
        {
            moveTarget = new Vector3(Random.Range(-moveVar, moveVar), 0, 1) * targetlength;
            moveTarget = transform.TransformPoint(moveTarget);
            Debug.DrawLine(transform.position, moveTarget, Color.red, 100);
        }
    }

    
    void FixedUpdate()
    {
        moveTarget.y = transform.position.y;
        transform.LookAt(moveTarget);
        rb.AddForce(transform.forward * moveForce, ForceMode.Impulse);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
