using System;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;

public class DoorsScript : MonoBehaviour
{
    [SerializeField]
    public enum DoorType
    {
        Normal_Door,
        Sun_Door
    }public DoorType type;

    public bool isOpen;
    public GameObject Pivot;
    public TextMeshProUGUI OpenText;
    public bool canOpen = false;
    private float targetRotationY;
    private int sunNums = 0;

    private void OnEnable()
    {
        PickUpSun.sunUIEvent += AddSuns;
    }

    void Start()
    {
        canOpen = false;
       // type = DoorType.Normal_Door;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canOpen)
        {
            OpenDoor();
        }
    }

    void AddSuns()
    {
        sunNums++;
    }

    public void OpenDoor()
    {
        if(type == DoorType.Sun_Door && sunNums == 2)
        {
            AudioManager.PlaySFX(SoundType.PUERTA);
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
        else if(type == DoorType.Normal_Door)
        {
            AudioManager.PlaySFX(SoundType.PUERTA);

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
        else
        {
            return;
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