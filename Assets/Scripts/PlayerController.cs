using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector3 moveDirection;
    public float moveSpeed = 2f;
    public float maxForwardSpeed = 8f;
    public float desiredForwardSpeed;
    public float forwardSpeed;

    const float groundAccel = 5f;
    const float groundDecel = 25f;

    //Animation
    Animator anim;

    bool IsMoveInput
    {
        get { return !Mathf.Approximately(moveDirection.sqrMagnitude, 0f); }
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    private void Move(Vector2 direction)
    {
        if(direction.sqrMagnitude > 1f) 
        {
            direction.Normalize();
        }

        desiredForwardSpeed = direction.magnitude * maxForwardSpeed;

        float acceleration = IsMoveInput ? groundAccel : groundDecel;

        forwardSpeed = Mathf.MoveTowards(forwardSpeed, desiredForwardSpeed, acceleration * Time.deltaTime);

        //Pass to Animator
        anim.SetFloat("ForwardSpeed", forwardSpeed);

        //transform.Translate(direction.x * moveSpeed * Time.deltaTime, 0, direction.y * moveSpeed * Time.deltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(moveDirection);
    }
}
