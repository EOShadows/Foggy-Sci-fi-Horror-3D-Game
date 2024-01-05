using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public GameObject projectile;
    public Transform owner;

    public void Fire(out Transform projForm)
    {
        GameObject newProjectile = Instantiate(projectile);
        newProjectile.transform.position = transform.position;
        var script = newProjectile.GetComponent<Projectile>();
        script.Initialize(transform.forward, owner);

        projForm = newProjectile.transform;
    }
}
