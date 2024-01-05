using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    private PlayerMovement pm;
    private PlayerInteraction pi;
    private PlayerCameraGuider pcg;
    private Rigidbody rb;

    private bool frozen = false;
    private bool canMove = true;
    private bool canInteract = true;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        pi = GetComponent<PlayerInteraction>();
        pcg = GetComponent<PlayerCameraGuider>();
        rb = GetComponent<Rigidbody>();
    } 

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateInteraction();
        UpdateLook();
    }

    // Handled seperately so they can be disabled / enabled from here.

    private void UpdateMovement()
    {
        if(!frozen || canMove)
            pm.UpdateMovement();
    }

    private void UpdateInteraction()
    {
        if (!frozen || canInteract)
            pi.UpdateInteraction();
    }
    
    private void UpdateLook()
    {
        pcg.UpdateLook();
    }

    private void OnDestroy()
    {
        Destroy(pm);
        Destroy(pi);
        Destroy(pcg);
        Destroy(rb);
    }
}
