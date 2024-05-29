using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;

    [Header("Jump")]
    public float jumpForce;
    public float chargeJumpForce;
    private float chargeTime = 0f;
    public float maxChargeTime;
    private bool isCharging = false;
    private bool isFullCharged = false;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;
    public bool canLook = true;

    public Action Inventory;

    [HideInInspector]
    private Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // ȭ�� �߾ӿ� Ŀ�� ����
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        if (isCharging)
        {
            chargeTime += Time.deltaTime;

            if (chargeTime >= maxChargeTime)
            {
                isFullCharged = true;
            }
            else
            {
                isCharging = false;
                isFullCharged = false;
                chargeTime = maxChargeTime;
            }
        }
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        // ����Ű�� ������ ���� ��
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        // ����Ű�� �� �̻� ������ ���� ��
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            // InputAction���� ������ ������� ȣ��, ���� ��¡�߿� �Ϲ� ���� ���� ����
            if (context.performed)
            {
                // ���������� ������ ���� ���
                if(context.interaction is HoldInteraction)
                {
                    StartCharging();
                }
                // �����⸸ ���� ���
                else if(context.interaction is PressInteraction)
                {
                    rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
                }
            }
            else if (context.canceled && isFullCharged)
            {
                ReleaseCharging();
            }
        }
    }

    private bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (-transform.forward * 0.2f) + transform.up * 0.01f, Vector3.down),
            new Ray(transform.position - transform.forward * 0.2f + transform.up * 0.01f, Vector3.down),
            new Ray(transform.position + transform.right * 0.2f + transform.up * 0.01f, Vector3.down),
            new Ray(transform.position - transform.right * 0.2f + transform.up * 0.01f, Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    private void Move()
    {
        // x, z ��ǥ ������
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = rb.velocity.y;

        rb.velocity = dir;
    }

    void CameraLook()
    {
        camCurRot += mouseDelta.y * lookSensitivity;
        camCurRot = Mathf.Clamp(camCurRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    // ��¡ ���� ���� Ȯ��
    public void StartCharging()
    {
        isCharging = true;
    }

    // ��¡ �ð��� á�� �� ��¡���� ����
    public void ReleaseCharging()
    {
        if (isFullCharged)
        {
            chargeTime = 0f;
            rb.AddForce(Vector2.up * chargeJumpForce, ForceMode.Impulse);
        }
        isCharging = false;
        isFullCharged = false;
    }
}
