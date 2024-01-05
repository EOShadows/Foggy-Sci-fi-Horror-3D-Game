using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private bool isDead = false;
    private string myType = "";

    private void Start()
    {
        myType = transform.tag;
    }

    public void kill()
    {
        isDead = true;
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    public string GetHealthType()
    {
        return myType;
    }
}
