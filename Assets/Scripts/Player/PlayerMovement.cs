using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed; //ship
    public float testFloat;
    public Camera sceneCamera;
    public PlayerAudio playerAudio;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 mousePosition;

    private bool bSpawnDust;
    private float timeBtwTrail;
    public float startTimeBtwTrail = 2f;
    private GameObject dustTrail;
    private string particlesToSpawn;
    private string dustTrailTag = "DustTrail";
    private string leafTrailTag = "LeafTrail";
    private string bloodSplatterTag = "BloodSplatter";

    private ObjectPooler objectPooler;


    private void Start() {
        objectPooler = ObjectPooler.Instance;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timeBtwTrail = startTimeBtwTrail;
        bSpawnDust = true;
    }

    void Update() {
        ProcessInputs();
        if (rb.velocity.magnitude > 0.1f)
        {
            SpawnDustTrail(particlesToSpawn);
        }
    }

    private void FixedUpdate() {
        Move();
    }

    void ProcessInputs() {
        float moveX = Input.GetAxisRaw("Horizontal"); // * Time.deltaTime ?
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    void Move() {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        animator.SetFloat("Speed", rb.velocity.magnitude);

        // rotate player along move direction
        if (rb.velocity.magnitude > 0.1f)
        {            
            Quaternion newRotation = transform.rotation;
            newRotation.SetLookRotation(new Vector3(moveDirection.x, moveDirection.y, testFloat).normalized, Vector3.back);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed);
        }

        // Rotate player to follow mouse
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MudFloor"))
        {
            particlesToSpawn = dustTrailTag; 
        }
        if (collision.CompareTag("BloodZone"))
        {
            if (bSpawnDust)
            {
                objectPooler.SpawnFromPool("BloodSplatter", transform.position, Quaternion.identity);
                bSpawnDust = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MudFloor"))
        {
            particlesToSpawn = leafTrailTag;
        }
        if (collision.CompareTag("BloodZone"))
        {
            bSpawnDust = true;
        }
    }

    private void SpawnDustTrail(string particlesToSpawn)
    {
        if (timeBtwTrail <= 0)
        {
            dustTrail = objectPooler.SpawnFromPool(particlesToSpawn, transform.position, Quaternion.identity);
            dustTrail.transform.SetParent(transform);
            timeBtwTrail = startTimeBtwTrail;
        }
        else
        {
            timeBtwTrail -= Time.deltaTime;
        }
    }
}
