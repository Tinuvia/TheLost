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

    private ObjectPooler objectPooler;


    private void Start() {
        objectPooler = ObjectPooler.Instance;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timeBtwTrail = startTimeBtwTrail;
    }

    void Update() {
        ProcessInputs();
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
        if (rb.velocity.magnitude > 0f)
        {            
            Quaternion newRotation = transform.rotation;
            newRotation.SetLookRotation(new Vector3(moveDirection.x, moveDirection.y, testFloat).normalized, Vector3.back);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed);
            SpawnDustTrail();
        }

        // Rotate player to follow mouse
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    private void SpawnDustTrail()
    {
        if (timeBtwTrail <= 0)
        {
            dustTrail = objectPooler.SpawnFromPool("DustTrail", this.transform.position, Quaternion.identity);
            dustTrail.transform.SetParent(this.transform);
            timeBtwTrail = startTimeBtwTrail;
        }
        else
        {
            timeBtwTrail -= Time.deltaTime;
        }
    }
}
