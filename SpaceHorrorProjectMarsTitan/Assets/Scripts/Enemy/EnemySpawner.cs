using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private float radius = 40;
    [SerializeField]
    private float distanceFromPlayer = 20;
    [SerializeField]
    private float distanceFromObj = 10;

    [SerializeField]
    private float reactionSpeed = 2;
    private float lastPosUpdate = 0;
    private bool isFollowingPlayer = true;

    [SerializeField]
    private float spawnInterval = 2;
    private float lastSpawnTime = 0;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        UpdatePosition();
        TrySpawn();
    }

    /// <summary>
    /// The position of the spawner is always following the player, but not
    /// always right on the player (Give the player the impression they could run away).
    /// 
    /// As in, the spawner lags in changing its position.
    /// </summary>
    private void UpdatePosition()
    {
        float lapse = Time.time - lastPosUpdate;

        if (lapse >= reactionSpeed)
        {
            isFollowingPlayer = !isFollowingPlayer;
            lastPosUpdate = Time.time;
        }

        if (isFollowingPlayer)
        {
            transform.position = player.position;
        }
    }

    private void TrySpawn()
    {
        float lapse = Time.time - lastSpawnTime;

        if (lapse >= spawnInterval)
        {
            Spawn();
            lastSpawnTime = Time.time;
        }
    }

    private void Spawn()
    {
        Vector3 position = GeneratePosition();
        var newEnemy = Instantiate(prefab);
        newEnemy.transform.position = position;
    }

    private Vector3 GeneratePosition()
    {
        Vector3 position = Vector3.zero;

        do
        {
            float x = Random.Range(-radius, radius) + transform.position.x;
            float z = Random.Range(-radius, radius) + transform.position.z;
            float y = transform.position.y;

            position = new Vector3(x, y, z);
        }
        while (PositionIsGood(position));

        return position;
    }

    private bool PositionIsGood(Vector3 position)
    {
        bool tooCloseToObj = Physics.CheckSphere(position, distanceFromObj, ~(1 << 7));
        bool tooCloseToPlayer = Physics.CheckSphere(position, distanceFromPlayer, (1 << 7));

        return (!tooCloseToObj && !tooCloseToPlayer);
    }
}
