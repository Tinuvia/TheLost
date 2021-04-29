using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float attackDamage;

    // MW

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Projectile entered collision");
        Attack(collision);
    }

    // added 210413 to prevent enemy attacks not counting while near player - but it doesn't work
    private void OnCollisionStay2D(Collision2D collision) {
        Debug.Log("Projectile inside collision");
        Attack(collision);
    }

    private void Attack(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(attackDamage);
            Debug.Log("Attack on enemy");
        }
        if(gameObject.activeSelf)
        {
            StartCoroutine("SetFalse", gameObject);
            Debug.Log("Projectile disabled from collision");
        }
    }

    IEnumerator SetFalse(GameObject objectToDisable)
    {

        yield return new WaitForSeconds(3.0f); // disable projectile after 1.0f secs
        if (objectToDisable.activeSelf)
        {
            objectToDisable.SetActive(false);
            Debug.Log("Projectile disabled from time");
        }
    }
}