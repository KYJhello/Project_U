using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private bool isSit = false;

    public void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void OnDisable()
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
        // �����ִ� ��ü�� ��ġ
        Vector3 lookPoint = Camera.main.transform.position + Camera.main.transform.forward * lookDistance;
        aimTarget.position = lookPoint;
        // �����ִ� ������ �������� ������ ������ 
        lookPoint.y = transform.position.y;
        // ������Ʈ ����
        transform.LookAt(lookPoint);
    }
    public void IsSit(bool isSit)
    {
        this.isSit = isSit;
        if (isSit)
        {
            cameraRoot.Translate(Vector3.down * 20f * Time.deltaTime);
        }
        else
        {
            cameraRoot.Translate(Vector3.up * 20f * Time.deltaTime);
        }
    }
    private void Look()
    {
        // ����
        yRotation += lookDelta.x * cameraSensitivity * Time.deltaTime;
        // ������
        xRotation -= lookDelta.y * cameraSensitivity * Time.deltaTime;
        // �ִ�ġ
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        // ī�޶� ��Ʈ�� ȸ���ϴ½�����
        cameraRoot.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
    private void OnLook(InputValue value)
    {
        lookDelta = value.Get<Vector2>();
    }
}
