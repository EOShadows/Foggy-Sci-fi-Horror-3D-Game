using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 velocity;
    private Rigidbody rb;

    [SerializeField]
    private float speed;

    private bool notActive = false;
    private Transform owner;
    private Transform tip;

    public virtual void Initialize(Vector3 direction, Transform owner)
    {
        this.owner = owner;
        velocity = direction * speed;
        rb = GetComponent<Rigidbody>();
        transform.forward = direction;
        rb.velocity = velocity;
    }

    public bool Grabbable(Transform requester)
    {
        return (owner == requester && notActive);
    }

    private void Update()
    {
        if(!notActive)
            transform.forward = rb.velocity.normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(notActive || collision.gameObject.tag == "Player")
            return;

        var health = collision.transform.GetComponent<Health>();

        if (health != null && health.GetHealthType() != "Player")
        {
            health.kill();

            AttachableObject attachable = collision.transform.GetComponent<AttachableObject>();

            if (attachable)
            {
                attachable.Attach(transform, collision.contacts[0].point);
                Freeze();
            }
            else
            {
                Deactivate();
            }
        }
        else
        {
            Freeze();
        }
    }

    private void Freeze()
    {
        Deactivate();
        Destroy(rb);
    }

    private void Deactivate()
    {
        rb.velocity = Vector3.zero;
        notActive = true;
    }

    public void UnFreeze()
    {
        gameObject.AddComponent<Rigidbody>();
    }
}
