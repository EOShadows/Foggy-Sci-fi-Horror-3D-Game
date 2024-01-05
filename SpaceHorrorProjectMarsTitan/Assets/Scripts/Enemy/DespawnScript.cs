using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnScript : MonoBehaviour
{

    [SerializeField]
    private float distanceForDespawn = 20;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        TryDespawn();
    }

    private void TryDespawn()
    {
        Vector3 playerPos = player.position;
        playerPos.y = transform.position.y;

        if (Vector3.Distance(transform.position, playerPos) >= distanceForDespawn)
        {
            foreach (var exception in GetComponentsInChildren<DoNotDespawn>())
            {
                exception.transform.parent = null;
                exception.React();
            }

            Destroy(gameObject);
        }
    }
}
