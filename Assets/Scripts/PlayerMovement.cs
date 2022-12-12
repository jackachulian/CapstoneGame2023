using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float turnSpeed = 90f;
    [SerializeField] private Animator animator;

    // If the player is sitting on a chair
    private bool sitting;
    // If the player is in transition between two locations (used for sitting, etc)
    private bool inTransition;
    // Player will continue to transition until this time is reached
    private float transitionUntil;
    // Speed to move when transitioning (Set on transition)
    private float transitionSpeed;
    // The position the player is transitioning towards
    private Transform transitionDestination;
    // The chair the player is on
    private GameObject chair;
    // Delay in seconds between positions when sitting down and standing up
    public float sitDelay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        animator.SetBool("inWalk", horizontalInput != 0 || verticalInput != 0);

        // Vector3 forward = Camera.main.transform.forward;
        // Vector3 right = Camera.main.transform.right;
        Vector3 forward = Vector3.forward;
        Vector3 right = Vector3.right;
        forward.y = 0;
        forward.Normalize();
        right.y = 0;
        right.Normalize();

        Vector3 direction = (forward*verticalInput + right*horizontalInput)*movementSpeed;
        direction.y = rb.velocity.y;

        if (direction.magnitude > 0.1f) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * turnSpeed);
        }

        rb.velocity = direction;

        // If in transition, move towards, and end transition if past time
        if (inTransition) 
        {
            transform.position = Vector3.MoveTowards(transform.position, transitionDestination.position, transitionSpeed*Time.deltaTime);
            if (Time.time > transitionUntil)
            {
                inTransition = false;
            }
        }

        // Interact
        if (Input.GetButtonDown("Fire1"))
        {
            // Sit in chair if not sitting and not in transition
            if (chair != null && !inTransition)
            {
                sitting = true;
                animator.SetBool("sitting", sitting);
                TransitionTo(chair.transform.Find("SitPos"), sitDelay);
            }
        }
    }

    void TransitionTo(Transform destination, float delay)
    {
        transitionUntil = Time.time + delay;
        transitionSpeed = (destination.position - transform.position).magnitude / delay;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chair"))
        {
            chair = collision.gameObject;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        chair = null;
    }
}
