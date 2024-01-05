using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To be used to make sure that when the DespawnScript is used, a child
/// object that you do not want to destory is not destroyed with the parent.
/// </summary>
public abstract class DoNotDespawn : MonoBehaviour
{
    public abstract void React();
}
