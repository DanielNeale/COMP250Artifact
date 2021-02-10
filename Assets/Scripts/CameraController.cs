using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float sensitivity;
    [SerializeField]
    private int maxY;
    [SerializeField]
    private int minY;

    private Transform cam;
    private float xMove;
    private float yMove;


    private void Start()
    {
        cam = transform.GetChild(0);
    }


    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(transform.forward.x, 0, transform.forward.z) * speed;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= new Vector3(transform.forward.x, 0, transform.forward.z) * speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(transform.right.x, 0, transform.right.z) * speed;
        }

        else if (Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(transform.right.x, 0, transform.right.z) * speed;
        }


        if (!(yMove - Input.GetAxis("Mouse Y") * sensitivity > maxY) && !(yMove - Input.GetAxis("Mouse Y") * sensitivity < minY))
        {
            yMove -= Input.GetAxis("Mouse Y") * sensitivity;
        }

        xMove += Input.GetAxis("Mouse X") * sensitivity;

        transform.rotation = Quaternion.Euler(0, xMove, 0);
        cam.rotation = Quaternion.Euler(yMove, xMove, 0);
    }
}
