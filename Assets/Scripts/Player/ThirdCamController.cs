using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdCamController : MonoBehaviour
{
    [SerializeField] Transform cameraRoot;
    [SerializeField] float cameraSensitivity;
    [SerializeField] float lookDistance;
    [SerializeField] Transform aimTarget;

    private Vector2 lookDelta;
    private float xRotation;
    private float yRotation;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    private void Update()
    {
        Rotate();
    }

    private void LateUpdate()
    {
        Look();
    }
    private void Rotate()
    {
        // 보고있는 물체의 위치
        Vector3 lookPoint = Camera.main.transform.position + Camera.main.transform.forward * lookDistance;
        aimTarget.position = lookPoint;
        // 보고있는 방향의 수직값은 현재의 수직값
        lookPoint.y = transform.position.y;
        // 룩포인트 보기
        transform.LookAt(lookPoint);
    }
    private void Look()
    {
        // 수평
        yRotation += lookDelta.x * cameraSensitivity * Time.deltaTime;
        // 수직값
        xRotation -= lookDelta.y * cameraSensitivity * Time.deltaTime;
        // 최대치
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        // 카메라 루트를 회전하는식으로
        cameraRoot.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
    private void OnLook(InputValue value)
    {
        lookDelta = value.Get<Vector2>();
    }
}
