using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    public Vector3 movement;
    public CinemachineVirtualCameraBase camera;
    private bool isMoving;


    private float X;
    private float Z;
    public bool isSound = false;

    public AudioManager audioManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        speed = 10f;
        if (controller == null)
        {
            controller = GetComponent<CharacterController>();
        }
        else
        {
            Debug.LogWarning("No se encontro el CharacterController");
        }

        if (camera == null)
        {
            camera = FindAnyObjectByType<CinemachineVirtualCameraBase>();
        }

        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
        else
        {
            Debug.LogWarning("No se encontro el AudioManager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        X = Input.GetAxisRaw("Horizontal");
        Z = Input.GetAxisRaw("Vertical");
        if (X == 0 && Z == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

        movement = transform.right * X + transform.forward * Z;
        controller.Move(movement.normalized * (speed * Time.deltaTime));
    }

    void Rotate()
    {
        float X = camera.GetComponent<CinemachinePanTilt>().PanAxis.Value;
        transform.rotation = Quaternion.Euler(0f, X, 0f);
    }

    public bool IsMove()
    {
        return isMoving;
    }
}