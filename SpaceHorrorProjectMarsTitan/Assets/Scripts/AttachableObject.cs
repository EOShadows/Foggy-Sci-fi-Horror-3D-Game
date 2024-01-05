using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttachableObject : MonoBehaviour
{
    public abstract void Attach(Transform obj, Vector3 from);
}
