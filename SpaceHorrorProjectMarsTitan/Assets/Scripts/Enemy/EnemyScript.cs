using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Transform body;
    private Rigidbody rb;
    private Transform player;
    private Health health;
    [SerializeField]
    private GameObject ragdollPrefab;
    [SerializeField]
    private GameObject splatterProj;
    [SerializeField]
    private GameObject splatterBurst;
    private ParticleSystem splatterBurstEffect;

    [SerializeField]
    private float speed = 1;

    private EnemyAttachable attachable;

    // Start is called before the first frame update
    void Start()
    {
        body = transform.GetChild(0).transform;
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = GetComponent<Health>();
        attachable = GetComponent<EnemyAttachable>();

        splatterBurstEffect = splatterBurst.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        SetDir();
        Move();
        CheckHealth();
    }

    private void SetDir()
    {
        Vector3 playerPos = player.position;
        playerPos.y = transform.position.y;
        body.LookAt(playerPos, Vector3.up);
    }

    private void Move()
    {
        Vector3 dir = body.forward;
        Vector3 movement = dir * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }

    private void CheckHealth()
    {
        if(health.GetIsDead())
        {
            Die();
        }
    }

    private void OnDrawGizmos()
    {
        
    }

    private void Die()
    {
        var ragdoll = Instantiate(ragdollPrefab);
        ragdoll.transform.forward = body.forward;
        ragdoll.transform.position = transform.position;
        var attach_info = attachable.GetAttached();
        DoAttach(ragdoll, attach_info);

        splatterBurst.transform.parent = null;
        splatterBurst.transform.position = attach_info.point;
        splatterBurst.SetActive(true);
        splatterBurstEffect.Play();

        var newSplat = Instantiate(splatterProj);
        newSplat.transform.position = transform.position + (Vector3.up * 4);

        Destroy(gameObject);
    }

    private void DoAttach(GameObject attachTo, EnemyAttachable.attach_info info)
    {
        if (!info.obj)
        {
            Debug.LogWarning("Attachable not found!");
            return;
        }

        var limbManager = attachTo.GetComponent<EnemyLimbs>();

        if (!limbManager)
        {
            Debug.LogError("EnemyScript: DoAttach(): EnemyLimbs script could not be found");
        }

        limbManager.Initialize();
        limbManager.AttachToLimb(info.obj, info.point);
    }
}
