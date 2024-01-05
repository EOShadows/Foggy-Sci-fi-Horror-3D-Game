using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int hitsSustained = 0;
    [SerializeField]
    private int hitsForKill = 3;

    private PlayerMain main;
    private PlayerMusicController music;

    // Start is called before the first frame update
    void Start()
    {
        main = GetComponent<PlayerMain>();
        music = GetComponent<PlayerMusicController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SustainDamage();
            CrackScreen();
        }
    }

    private void SustainDamage()
    {
        hitsSustained++;

        if (hitsSustained <= hitsForKill)
            SoundPlayer.GetInstance().PlaySound("glass_break", transform.position, 0.5f);

        if (hitsSustained >= hitsForKill)
        {
            Kill();
        }
    }

    private void CrackScreen()
    {
        var screen = FindAnyObjectByType<DamageScreen>();
        if (screen)
        {
            screen.CrackScreen();
        }
    }

    private void Kill()
    {
        music.SwitchToDead();
        transform.Rotate(transform.right, -90);
        Destroy(main);
    }
}
