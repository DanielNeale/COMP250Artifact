using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBase : MonoBehaviour
{
    [SerializeField]
    private float moveForce = .5f;
    [SerializeField]
    private float maxSpeed = 3;
    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }



    void FixedUpdate()
    {
        Vector3 newTarget = MoveTarget();

        if (!(newTarget == Vector3.zero))
        {
            transform.LookAt(newTarget);

            rb.AddForce(transform.forward * moveForce, ForceMode.Impulse);

            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }


    protected virtual Vector3 MoveTarget()
    {
        return Vector3.zero;
    }
}
