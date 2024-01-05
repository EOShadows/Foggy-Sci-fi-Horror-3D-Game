using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLimb : MonoBehaviour
{
   public void Attach(Transform attachable, Vector3 point)
   {
        attachable.transform.position = point;
        attachable.forward = (transform.position - attachable.position);
        attachable.transform.SetParent(transform);
    }
}
