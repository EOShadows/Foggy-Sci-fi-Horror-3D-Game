using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMusicController : MonoBehaviour
{
    [SerializeField]
    private GameObject normal;
    [SerializeField]
    private GameObject dead;

    public void SwitchToDead()
    {
        normal.SetActive(false);
        dead.SetActive(true);
    }
}
