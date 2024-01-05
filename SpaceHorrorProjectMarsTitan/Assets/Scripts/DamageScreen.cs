using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScreen : MonoBehaviour
{

    private int crackIndex = 0;

    public GameObject[] regularCracks;
    public GameObject[] bloodCracks;

    private void Start()
    {
        ShuffleCracks();
    }

    public void CrackScreen()
    {
        var regLength = regularCracks.Length;

        if (crackIndex < regLength)
        {
            regularCracks[crackIndex].SetActive(true);
        }
        else if (crackIndex - regLength < bloodCracks.Length)
        {
            bloodCracks[crackIndex - regLength].SetActive(true);
        }

        crackIndex++;
    }

    private void ShuffleCracks()
    {
        var newCracks = new GameObject[regularCracks.Length];
        for (int i = 0; i < regularCracks.Length; i++)
        {
            int index = Random.Range(0, regularCracks.Length);

            if(newCracks[index] == null)
            {
                newCracks[index] = regularCracks[i];
            }
            else
            {
                i--;
            }
        }

        regularCracks = newCracks;

        var newBloodCracks = new GameObject[bloodCracks.Length];
        for (int i = 0; i < bloodCracks.Length; i++)
        {
            int index = Random.Range(0, bloodCracks.Length);

            if (newBloodCracks[index] == null)
            {
                newBloodCracks[index] = bloodCracks[i];
            }
            else
            {
                i--;
            }
        }

        bloodCracks = newBloodCracks;
    }
}
