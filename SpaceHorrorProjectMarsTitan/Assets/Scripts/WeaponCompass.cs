using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCompass : MonoBehaviour
{
    private Transform player;
    private Transform playerHead;
    private Transform weapon;
    private bool weaponThrown = false;

    [SerializeField]
    private Transform rotatable;
    [SerializeField]
    private GameObject compass;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHead = player.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponThrown)
        {
            Vector3 dir = playerHead.position - new Vector3(weapon.position.x, playerHead.position.y, weapon.position.z);
            float angle = Vector3.SignedAngle(dir, playerHead.forward, Vector3.up);
            rotatable.localEulerAngles = new Vector3(0, 0, angle);
        }

        if (compass.activeSelf != weaponThrown)
        {
            compass.SetActive(weaponThrown);
        }
    }

    public void FindWeapon(Transform weapon)
    {
        weaponThrown = true;
        this.weapon = weapon;
    }

    public void NotifyFound()
    {
        weaponThrown = false;
        this.weapon = null;
    }
}
