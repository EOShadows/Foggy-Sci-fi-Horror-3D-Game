using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private AttackManager attack;

    // Start is called before the first frame update
    void Start()
    {
        attack = GetComponent<AttackManager>();
    }

    // Update is called once per frame
    public void UpdateInteraction()
    {
        attack.UpdateAttack();
    }

    private void OnTriggerEnter(Collider other)
    {
        var projComp = other.GetComponent<Projectile>();

        if (projComp && projComp.Grabbable(transform))
        {
            attack.retrieveWeapon();
            Destroy(other.gameObject);
        }
    }
}
