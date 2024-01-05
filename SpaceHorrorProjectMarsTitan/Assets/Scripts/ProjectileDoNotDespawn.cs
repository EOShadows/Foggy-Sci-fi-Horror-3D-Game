using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDoNotDespawn : DoNotDespawn
{
    private Projectile mainScript;

    private void Start()
    {
        mainScript = GetComponent<Projectile>();
    }

    public override void React()
    {
        mainScript.UnFreeze();
    }
}
