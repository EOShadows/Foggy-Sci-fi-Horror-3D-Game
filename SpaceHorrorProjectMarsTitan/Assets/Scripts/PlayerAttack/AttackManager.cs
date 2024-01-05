using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField]
    private GameObject visualKnife;

    private AttackScript attack;
    private bool hasWeapon = true;

    // Start is called before the first frame update
    void Start()
    {
        attack = GetComponentInChildren<AttackScript>();
    }

    public void retrieveWeapon()
    {
        hasWeapon = true;

        var finder = FindAnyObjectByType<WeaponCompass>();
        if (finder)
        {
            finder.NotifyFound();
        }
    }

    public void UpdateAttack()
    {
        UpdateThrow();

        UpdateKnifeVisual();
    }

    private void UpdateThrow()
    {
        if (!hasWeapon)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            Transform projForm;
            attack.Fire(out projForm);
            hasWeapon = false;

            var finder = FindAnyObjectByType<WeaponCompass>();
            if (finder)
            {
                finder.FindWeapon(projForm);
            }
        }
    }

    private void UpdateKnifeVisual()
    {
        if(visualKnife.activeSelf != hasWeapon)
        {
            visualKnife.SetActive(hasWeapon);
        }
    }
}
