using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoliageMoverRotate : MonoBehaviour
{
    public AudioClip[] foliageRustle;
    public float rotationSpeed = 10f;
    public float maxRotation = 5f;
    private AudioSource audioS;
    private bool isRotating = false;


    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isRotating)
            RotateSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isRotating = true;
            audioS.pitch = Random.Range(0.6f, 1.5f);
            audioS.volume = Random.Range(0.6f, 1.4f);
            int r = Random.Range(0, foliageRustle.Length);
            audioS.PlayOneShot(foliageRustle[r]);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isRotating = false;
        }
    }

    void RotateSprite()
    {
        transform.Rotate(0f, 0f, maxRotation * Mathf.Sin(Time.time * rotationSpeed), Space.World);
    }
}
