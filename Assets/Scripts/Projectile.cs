using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float attackDamage;

    private void OnCollisionEnter2D(Collision2D collision) {
        // Debug.Log("Projectile entered collision");
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(attackDamage);
            Debug.Log("Attack on enemy");
        }

        if(collision.gameObject.tag == "DamageableObject")
        {
            collision.gameObject.GetComponent<DamageableObjects>().TakeDamage(attackDamage);
            Debug.Log("Attack on DamageableObject");
        }

        gameObject.SetActive(false);
        //Debug.Log("Projectile disabled from collision");
    }
}