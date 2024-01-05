using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraGuider : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 10f;
    [SerializeField]
    private float UpConstraint = 315f;
    [SerializeField]
    private float DownConstraint = 45f;

    private Vector2 rotation = Vector2.zero;
    private Transform head;
    private Transform caster;

    // Start is called before the first frame update
    void Start()
    {
        head = transform.GetChild(0).transform;
        caster = transform.GetChild(1).transform;
    }

    // Update is called once per frame
    public void UpdateLook()
    {
        if (Cursor.visible || Cursor.lockState != CursorLockMode.Confined)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }

        bool rotationWasConstrained = false;
        var oldRotation = rotation;

        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");

        head.eulerAngles = (Vector2)rotation * sensitivity;
        Vector3 newRotation = GetEulerConstrained(head.eulerAngles, out rotationWasConstrained);
        head.eulerAngles = newRotation;
        newRotation.x = caster.eulerAngles.x;
        caster.eulerAngles = newRotation;
        

        if (rotationWasConstrained)
        {
            rotation = oldRotation;
        }
    }

    private Vector2 GetEulerConstrained(Vector3 v, out bool constrained)
    {
        Vector3 v2 = v;
        constrained = false;

        if (v.x < 135)
        {
            if (v2.x > DownConstraint)
            {
                constrained = true;
                v2.x = DownConstraint;
            }
        }
        else if (v.x > 225)
        {
            if (v2.x < UpConstraint)
            {
                constrained = true;
                v2.x = UpConstraint;
            }
        }

        return v2;
    }
}
