using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float attackDamage;

    // MW

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(attackDamage);
            Debug.Log("Attack on enemy");
        }
        gameObject.SetActive(false);
    }


    /*
    // BMo
    private void OnTriggerEnter2D(Collider2D collision) {
        switch(collision.gameObject.tag) {
            case "Wall":
                Destroy(gameObject);
                break;
            case "Enemy":
                
                // ADD DamageValue of projectile
                Destroy(gameObject);
                break;
        }
    }
    */
}
