using System;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    [SerializeField] private float rayDistance;

    public static Action seeEvent;
    private Camera playerCamera;

    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        ShootRaycast();
    }

    void ShootRaycast()
    {
        // Posicion y direccion del raycast
        Vector3 rayOrigin = playerCamera.transform.position;
        Vector3 rayDirection = playerCamera.transform.forward;

        RaycastHit hit;

        // Lanzar el raycast
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayDistance) && hit.collider.tag == "TP")
        {
            seeEvent?.Invoke();
        }
    }

    // Debug
    void OnDrawGizmos()
    {
        if (playerCamera != null)
        {
            Gizmos.color = Color.red;
            Vector3 start = playerCamera.transform.position;
            Vector3 end = start + playerCamera.transform.forward * rayDistance;
            Gizmos.DrawLine(start, end);
        }
    }
}