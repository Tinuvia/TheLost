using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3f;

    [Header("Attack")]
    
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackSpeed = 1f;
    private float canAttack;

    [Header("Health")]
    private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private float delayDestroy = 4f;


    public Animator animator;
    public Transform target;
    public GameObject player;


    private void Start() {
        health = maxHealth;
    }


    private void FixedUpdate() {
        if (target != null) {
            Move();
        }
    }

    void Move() {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg - 90f);
    }

    private void OnCollisionStay2D(Collision2D collision) {
        AttackPlayer(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        AttackPlayer(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player" && health > 0) {
            FollowPlayer(collision, true);
        } else
            FollowPlayer(collision, false);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            FollowPlayer(collision, false);
        }
    }

    public void TakeDamage(float dmg) {
        health -= dmg;
        Debug.Log("Enemy health: " + health);

        if ((target == null) && health > 0f)
        {
            target = player.transform;
            animator.SetBool("IsMoving", true);
        }
         // to get access to player, better to subscribe to the projectile's OnCollision?

        if (health <= 0f) {
            animator.SetBool("IsDead", true);
            animator.SetBool("IsMoving", false);
            Debug.Log("Enemy dead");
            target = null;

            // Instead, replace with lootable object that is deactivated/destroyed after some time
            StartCoroutine("RemoveEnemyAfterTime", delayDestroy);
        }
    }

    private void AttackPlayer(Collision2D collision) {
        Debug.Log("Attack on player started");
        if (collision.gameObject.tag == "Player") {
            if (attackSpeed <= canAttack) {
                collision.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
                canAttack = 0f;
                Debug.Log("Player attacked");
            } else {
                canAttack += Time.deltaTime;
            }
        }
    }

    private void FollowPlayer(Collider2D collision, bool bFollow)
    {
        if (bFollow)  {
            target = collision.transform;
        } else {
            target = null;
        }        
        animator.SetBool("IsMoving", bFollow);
    }

    IEnumerator RemoveEnemyAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}