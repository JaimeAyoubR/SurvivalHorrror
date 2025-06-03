using System;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DoorsScript : MonoBehaviour
{
    [SerializeField]
    public enum DoorType
    {
        Normal_Door,
        Sun_Door
    }

    [Header("Door Configuration")]
    public DoorType type;
    public GameObject Pivot;
    public TextMeshProUGUI OpenText;

    [Header("Sun Door Settings")]
    public int requiredSuns = 2;

    [Header("Door Status")]
    public bool isOpen = true;
    public bool canOpen = false;

    private float targetRotationY;
    private int sunNums = 0;

    private void OnEnable()
    {
        PickUpSun.sunUIEvent += AddSuns;
    }

    private void OnDisable()
    {
        PickUpSun.sunUIEvent -= AddSuns;
    }

    void Start()
    {
        canOpen = false;
        UpdateUIText();
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

        if (canOpen)
        {
            UpdateUIText();
        }
    }

    public void OpenDoor()
    {
        if (!CanOpenDoor())
        {
            ShowCannotOpenMessage();
            return;
        }

        //AudioManager.PlaySFX(SoundType.PUERTA);
        AnimateDoor();
        UpdateUIText();
    }

    private bool CanOpenDoor()
    {
        switch (type)
        {
            case DoorType.Normal_Door:
                //isOpen = true;
                return true;

            case DoorType.Sun_Door:
                return sunNums >= requiredSuns;

            default:
                Debug.LogError($"Unknown door type: {type}");
                return false;
        }
    }

    private void AnimateDoor()
    {
        DOTween.Kill(Pivot.transform);

        if (!isOpen)
        {
            targetRotationY = Pivot.transform.rotation.y - 90f;
            Pivot.transform.DORotate(new Vector3(0, 0, 0), 0.2f, RotateMode.Fast)
                .OnComplete(() => Debug.Log($"{type} opened"));
            isOpen = true;
        }
        else
        {
            targetRotationY = Pivot.transform.rotation.y + 90f;
            Pivot.transform.DORotate(new Vector3(0, 90, 0), 0.2f, RotateMode.Fast)
                .OnComplete(() => Debug.Log($"{type} closed"));
            isOpen = false;
        }
    }

    private void UpdateUIText()
    {
        if (!canOpen || OpenText == null) return;

        switch (type)
        {
            case DoorType.Normal_Door:
                OpenText.text = isOpen ? "Close door: E" : "Open door: E";
                break;

            case DoorType.Sun_Door:
                if (sunNums >= requiredSuns)
                {
                    OpenText.text = isOpen ? "Close door: E" : "Open door: E";
                }
                else
                {
                    OpenText.text = $"Need {requiredSuns - sunNums} more suns";
                }
                break;
        }
    }

    private void ShowCannotOpenMessage()
    {
        switch (type)
        {
            case DoorType.Sun_Door:
                Debug.Log($"Cannot open Sun Door. Need {requiredSuns - sunNums} more suns.");
                break;

            default:
                Debug.Log("Cannot open door: Unknown condition.");
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!canOpen)
            {
                canOpen = true;
                OpenText.enabled = true;
                UpdateUIText();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = false;
            if (OpenText != null)
            {
                OpenText.enabled = false;
            }
        }
    }
    public string GetDoorStatus()
    {
        return $"Type: {type}, Open: {isOpen}, Suns: {sunNums}/{requiredSuns}, Can Open: {CanOpenDoor()}";
    }
}