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
    public bool canOpen = false;
    private float targetRotationY;


    void Start()
    {
        canOpen = false;
    }
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
            DOTween.KillAll();
            targetRotationY = Pivot.transform.rotation.y - 90f;
            Pivot.transform.DORotate(new Vector3(0, 0, 0), 0.2f, RotateMode.Fast);
            isOpen = true;
        }
        else
        {
            DOTween.KillAll();
            targetRotationY = Pivot.transform.rotation.y + 90f;
            Pivot.transform.DORotate(new Vector3(0, 90, 0), 0.2f, RotateMode.Fast);
            isOpen = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.tag == "Player")
        {
            OpenText.enabled = true;
            canOpen = true;
            OpenText.text = isOpen ? "Close door: E" : "Open door: E";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canOpen = false;
        OpenText.enabled = false;
    }
}