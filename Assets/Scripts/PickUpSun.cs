using System;
using UnityEngine;

public class PickUpSun : MonoBehaviour
{
   public GameManager  gameManager;
    public static Action sunUIEvent;

    public bool canPickUp = false;
    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindAnyObjectByType<GameManager>();
        }
    }

    void Update()
    {
        if (canPickUp)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                sunUIEvent?.Invoke();
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
            canPickUp = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canPickUp = false;
    }
    
}
