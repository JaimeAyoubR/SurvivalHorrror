using System;
using UnityEngine;

public class PickUpSun : MonoBehaviour
{
   public GameManager  gameManager;
    public bool canPickUp = false;
    void Start()
    {
        if (gameManager == null)
        {
            gameManager = GameObject.FindAnyObjectByType<GameManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickUp)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                gameManager.addSun();
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
