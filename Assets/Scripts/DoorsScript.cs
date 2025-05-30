using System;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;

public class DoorsScript : MonoBehaviour
{
    public bool isOpen;
    public GameObject Pivot;
    public TextMeshProUGUI OpenText;
    public bool canOpen = true;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canOpen)
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            Pivot.transform.DORotate(new Vector3(0, -90, 0), 0.2f, RotateMode.Fast);
            isOpen = true;
        }
        else
        {
            Pivot.transform.DORotate(new Vector3(0, 0, 0), 0.2f, RotateMode.Fast);
            isOpen = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            OpenText.enabled = true;
           canOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canOpen = false;
        OpenText.enabled = false;
    }
 
}