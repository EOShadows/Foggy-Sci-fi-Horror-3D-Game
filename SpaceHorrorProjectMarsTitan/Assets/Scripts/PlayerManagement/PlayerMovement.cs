using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float walkSpeed = 1;
    [SerializeField]
    private float runSpeed = 1.5f;
    private float curSpeed;

    private bool running = false;
    private bool isIdle = true;

    private float timeStartedWalking = 0;
    [SerializeField]
    private float walkLapseInterval = 1;

    private Transform head;
    private Vector3 origin;

    [SerializeField]
    private AnimationCurve bobMotion;

    private bool isGrounded = true;
    private bool isJumping = true;
    [SerializeField]
    private Vector2 jumpForce = Vector3.up * 2;
    [SerializeField]
    private float groundedSensitivity = 0.2f;
    [SerializeField]
    private float movementRestraintInJumpMax = 0.5f;
    [SerializeField]
    private float movementRestraintInJumpMin = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        head = transform.GetChild(0).GetComponent<Transform>();
        origin = head.localPosition;

        UpdateSpeed();
    }

    public void UpdateMovement()
    {
        UpdateRun();
        Vector3 movementRaw = (GetForwardInput() + GetSideInput()).normalized;
        UpdateJump(movementRaw);

        MoveBody(movementRaw);
        UpdateIdle(movementRaw);
        UpdateBobbingMotion();
    }

    private Vector3 GetForwardInput()
    {
        float forwardInput = Input.GetAxisRaw("Vertical");
        Vector3 forward = head.forward;
        Vector3 newMovement = (Vector3.ProjectOnPlane(forward, Vector3.up)).normalized * forwardInput;
        return newMovement;
    }

    private Vector3 GetSideInput()
    {
        float sideInput = Input.GetAxisRaw("Horizontal");
        Vector3 side = head.right;
        Vector3 newMovement = side.normalized * sideInput;
        return newMovement;
    }

    private void UpdateRun()
    {
        running = Input.GetButton("Run");
        UpdateSpeed();
    }

    private void MoveBody(Vector3 movementRaw)
    {
        var directionConstraint = Mathf.Abs(Mathf.Abs(Vector3.Dot(movementRaw, Vector3.ProjectOnPlane(rb.velocity.normalized, Vector3.up))) - 1);

        Vector3 movement = movementRaw * curSpeed * Time.deltaTime * 
            ((!isGrounded) ? Mathf.Max(movementRestraintInJumpMin, movementRestraintInJumpMax * directionConstraint) : 1);
        rb.MovePosition(transform.position + movement);
    }

    private void UpdateSpeed()
    {
        curSpeed = (!running) ? walkSpeed : runSpeed;
    }

    private void UpdateIdle(Vector3 movementRaw)
    {
        bool before = isIdle;
        isIdle = movementRaw == Vector3.zero;

        if(!isIdle && before != isIdle)
        {
            timeStartedWalking = Time.time;
        }
    }

    private void UpdateBobbingMotion()
    {
        if (!isIdle && isGrounded)
        {
            float timeLapse = Time.time - timeStartedWalking;
            float intervalNow = walkLapseInterval / (curSpeed / walkSpeed);
            float intervalLapse = (timeLapse % intervalNow) / intervalNow;
            head.localPosition = origin + ((Vector3.up * bobMotion.Evaluate(intervalLapse)) * (curSpeed / walkSpeed));
        }
    }

    private void UpdateJump(Vector3 movementRaw)
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            DoJump(movementRaw);
        }
    }

    private void DoJump(Vector3 movementRaw)
    {
        var xForce = movementRaw * jumpForce.x * ((running) ? (runSpeed / walkSpeed) : 1);
        var yForce = (Vector3.up * jumpForce.y);

        rb.AddForce(yForce + xForce, ForceMode.Impulse);
        isJumping = true;
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        List<ContactPoint> contacts = new List<ContactPoint>();
        int hits = collision.GetContacts(contacts);

        foreach (var contact in collision.contacts)
        {
            if (Mathf.Abs(Vector3.Dot(contact.normal, Vector3.up) - 1) < groundedSensitivity)
            {
                isGrounded = true;
                isJumping = false;
            }
        }
    }
}
