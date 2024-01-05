using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttachable : AttachableObject
{
    private Transform attachedObj;
    private Vector3 fromPoint;

    public struct attach_info
    {
        public Transform obj;
        public Vector3 point;

        public attach_info(Transform obj, Vector3 point)
        {
            this.obj = obj;
            this.point = point;
        }
    }

    public override void Attach(Transform obj, Vector3 from)
    {
        attachedObj = obj;
        fromPoint = from;
    }

    public attach_info GetAttached()
    {
        return new attach_info(attachedObj, fromPoint);
    }
}
