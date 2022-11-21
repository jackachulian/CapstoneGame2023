using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementSpeed = 10f;
    private Animator animator;


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
        if (horizontalInput != 0 || verticalInput != 0){
            animator.SetBool("inWalk",true);
        }
        else{
            animator.SetBool("inWalk",false);
        }

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        forward.Normalize();
        right.y = 0;
        right.Normalize();

        Vector3 direction = (forward*verticalInput + right*horizontalInput)*movementSpeed;
        direction.y = rb.velocity.y;

        rb.velocity = direction;
    }
}
