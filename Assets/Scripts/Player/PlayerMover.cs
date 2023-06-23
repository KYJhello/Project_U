using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpSpeed;

    private CharacterController controller;
    Vector3 moveDir;
    private float ySpeed = 0;

    private float moveSpeed;
    private bool isRun;
    private Animator animator;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        // �ȿ�����
        if (moveDir.magnitude == 0)  // �ȿ�����
        {
            moveSpeed = Mathf.Lerp(moveSpeed, 0, 0.5f); // ���� ����
        }
        else if (isRun)       // ��  
        {
            moveSpeed = Mathf.Lerp(moveSpeed, runSpeed, 0.5f);
        }
        else                   // ����     
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, 0.5f);
        }


        controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
        controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);

        //Mathf.Lerp();
        animator.SetFloat("XSpeed", moveDir.x, 0.1f, Time.deltaTime);
        animator.SetFloat("YSpeed", moveDir.z, 0.1f, Time.deltaTime);
        animator.SetFloat("Speed", moveSpeed);
    }
    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveDir = new Vector3(input.x, 0, input.y);
    }

    private void OnRun(InputValue value)
    {
        isRun = value.isPressed;
    }

    private void Jump()
    {
        ySpeed += Physics.gravity.y * Time.deltaTime;

        // �ٴڿ� �ְ� �ϰ����̶��
        if (IsGrounded() && ySpeed < 0)
        {
            ySpeed = -1;
            animator.SetBool("IsJump", false);
        }

        controller.Move(Vector3.up * ySpeed * Time.deltaTime);
    }
    private void OnJump(InputValue value)
    {
        if (IsGrounded())
        {
            animator.SetBool("IsJump", true);
            ySpeed = jumpSpeed;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        return Physics.SphereCast(transform.position + Vector3.up * 1f,
            0.5f, Vector3.down, out hit, 0.6f);
    }
}
