using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : AgentBase
{
    [SerializeField]
    private float moveVar = 0.1f;
    [SerializeField]
    private float targetlength = 1.5f;
    public bool returnBack = false;
    private Stack<Vector3> returnNodes = new Stack<Vector3>();


    protected override Vector3 MoveTarget()
    {
        Vector3 target = Vector3.zero;

        if (returnBack && returnNodes.Count > 0)
        {
            target = returnNodes.Pop();
        }

        else
        {
            returnBack = false;
            target = new Vector3(Random.Range(-moveVar, moveVar), 0, 1) * targetlength;
            target = transform.TransformPoint(target);
            Debug.DrawLine(transform.position, target, Color.red, 100);
        }

        return target;
    }
}
