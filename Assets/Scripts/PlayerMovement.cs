using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float turnSpeed = 90f;
    [SerializeField] private Animator animator;


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
    }
}
