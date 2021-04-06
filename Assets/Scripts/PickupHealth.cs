using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHealth : MonoBehaviour
{
    [SerializeField] private float healthBonus;
    private PlayerHealth playerHealthScript;
    private float healthToGive;
    private float maxHealth;

    void Start()
    {
        playerHealthScript = GameObject.Find("Player").GetComponent<PlayerHealth>();
        healthToGive = playerHealthScript.maxHealth * (healthBonus / 100f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealthScript.UpdateHealth(healthToGive);
            // When OBJECT POOLING - setactive(false)
            Destroy(gameObject);
        }
    }
}
