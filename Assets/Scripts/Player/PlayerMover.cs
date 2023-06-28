using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    private PlayerData data;
    private CharacterController controller;
    private Vector3 moveDir;
    public Vector3 MoveDir { get; private set; }
    private float zSpeed = 0; // 위 아래


    private bool isRun;
    private bool isSit;
    private bool isRoll;

    public Coroutine moveRoutine;
    public Coroutine jumpRoutine;
    public Coroutine rollRoutine;

    private void Awake()
    {
        data = GetComponent<PlayerData>();
        controller = GetComponent<CharacterController>();
    }

    public void Move()
    {
        moveRoutine = StartCoroutine(MoveRoutine());
    }
    public void Jump()
    {
        jumpRoutine = StartCoroutine(JumpRoutine());
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            // 안움직임
            if (moveDir.magnitude == 0)  // 안움직임
            {
                data.MoveSpeed = Mathf.Lerp(data.MoveSpeed, 0, 0.5f); // 선형 보간
            }
            else if (isSit)      // 앉음
            {
                data.MoveSpeed = Mathf.Lerp(data.MoveSpeed, data.CrouchSpeed, 0.5f);
            }
            else if (isRun)       // 뜀  
            {
                data.MoveSpeed = Mathf.Lerp(data.MoveSpeed, data.RunSpeed, 0.5f);
            }
            else                   // 걸음     
            {
                data.MoveSpeed = Mathf.Lerp(data.MoveSpeed, data.WalkSpeed, 0.5f);
            }


            controller.Move(transform.forward * moveDir.z * data.MoveSpeed * Time.deltaTime);
            controller.Move(transform.right * moveDir.x * data.MoveSpeed * Time.deltaTime);

            //Mathf.Lerp();
            data.Anim.SetFloat("XSpeed", moveDir.x, 0.1f, Time.deltaTime);
            data.Anim.SetFloat("YSpeed", moveDir.z, 0.1f, Time.deltaTime);
            data.Anim.SetFloat("Speed", data.MoveSpeed);
            yield return null;
        }
    }

    IEnumerator RollRoutine()
    {
        while (true)
        {
            controller.Move(transform.forward * 1f * data.RollSpeed * Time.deltaTime);
            controller.Move(transform.right * moveDir.x * data.RollSpeed * Time.deltaTime);
            yield return null;
        }

    }
    IEnumerator JumpRoutine()
    {
        while (true)
        {
            zSpeed += Physics.gravity.y * Time.deltaTime;

            // 바닥에 있고 하강중이라면
            if (IsGrounded() && zSpeed < 0)
            {
                zSpeed = -1;
                data.Anim.SetBool("IsJump", false);
                data.Anim.SetBool("IsFloat", false);
            }
            else if (!IsGrounded() && IsFloat() && zSpeed < -3 && zSpeed > -5)
            {
                data.Anim.SetTrigger("Floating");
                data.Anim.SetBool("IsFloat", true);
            }

            data.Anim.SetFloat("ZSpeed", zSpeed);
            controller.Move(Vector3.up * zSpeed * Time.deltaTime);

            yield return null;
        }
    }
    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveDir = new Vector3(input.x, 0, input.y);
    }

    private void OnRun(InputValue value)
    {
        if (data.Anim.GetBool("IsJump"))
        {
            return;
        }
        isRun = value.isPressed;
    }
    private void OnSit(InputValue value)
    {
        if (isSit) { isSit = false; }
        else { isSit = true; }

        data.Anim.SetBool("Sit", isSit);
    }

    private void OnRoll(InputValue value)
    {
        data.Anim.SetTrigger("Roll");
        rollRoutine = StartCoroutine(RollRoutine());
        StopCoroutine(rollRoutine);
    }

  

    private void OnJump(InputValue value)
    {
        if (isSit)
        {
            return;
        }
        if (IsGrounded())
        {
            data.Anim.SetBool("IsJump", true);
            data.Anim.SetTrigger("JumpStart");
            zSpeed = data.JumpSpeed;
        }
        Jump();
    }
    private bool IsFloat()
    {
        RaycastHit hit;
        return !Physics.Raycast(transform.position, Vector3.down, out hit, 3f);
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        return Physics.SphereCast(transform.position + Vector3.up * 1f,
            0.5f, Vector3.down, out hit, 0.6f);
    }
}
