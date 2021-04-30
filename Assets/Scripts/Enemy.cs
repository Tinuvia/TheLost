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

    [Header("Audio")]
    [SerializeField] private AudioSource audioS;
    [SerializeField] private AudioClip detectScream;
 
    public Transform target;
    public GameObject player; // only works in this scene? if so, instead intitalize in start as usual

    Animator animator;
    int isMovingHash;

    private void Start() {
        health = maxHealth;
        animator = GetComponent<Animator>();
        isMovingHash = Animator.StringToHash("isMoving");
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
        animator.SetTrigger("Wounded");

        if ((target == null) && health > 0f)
        {
            target = player.transform;
            animator.SetBool(isMovingHash, true);
        }
         // to get access to player, better to subscribe to the projectile's OnCollision?

        if (health <= 0f) {
            animator.SetTrigger("Dying");
            animator.SetBool(isMovingHash, false);
            target = null;

            // Instead, replace with lootable object that is deactivated/destroyed after some time
            //StartCoroutine("RemoveEnemyAfterTime", delayDestroy);
        }
    }

    private void AttackPlayer(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            if (attackSpeed <= canAttack) {
                collision.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
                animator.SetTrigger("Attack");
                canAttack = 0f;
            } else {
                canAttack += Time.deltaTime;
            }
        }
    }

    private void FollowPlayer(Collider2D collision, bool bFollow)
    {
        if (bFollow)  {
            target = collision.transform;
            audioS.pitch = Random.Range(0.9f, 1.1f);
            audioS.volume = Random.Range(0.8f, 1f);
            audioS.PlayOneShot(detectScream);
        } else {
            target = null;
        }        
        animator.SetBool(isMovingHash, bFollow);
    }

    IEnumerator RemoveEnemyAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}