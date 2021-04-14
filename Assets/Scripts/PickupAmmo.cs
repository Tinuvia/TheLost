using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAmmo : MonoBehaviour
{
    [SerializeField] private int ammoCount;
    private PlayerAttack playerAttackScript;
    public AudioClip reloadSound;

    void Start()
    {
        playerAttackScript = GameObject.Find("Player").GetComponent<PlayerAttack>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(reloadSound, transform.position);
            playerAttackScript.UpdateAmmo(ammoCount);
            // When OBJECT POOLING - setactive(false)
            Destroy(gameObject);
        }
    }
}
