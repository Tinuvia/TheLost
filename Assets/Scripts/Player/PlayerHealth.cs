using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health = 0f;
    private Animator animator;

    // After Command Pattern, set maxHealth to private
    [SerializeField] public float maxHealth = 100f;
    [SerializeField] private Slider healthSlider;

    private void Start() {
        animator = GetComponent<Animator>();
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
    }

    public void UpdateHealth(float mod) {
        health += mod;
        if(mod < 0)
        {
            animator.SetTrigger("Wounded");
        }


        if (health > maxHealth) {
            health = maxHealth;
        } else if (health <= 0f) {
            health = 0f;
            healthSlider.value = health;
            PlayerDied();
        }
    }

    private void PlayerDied()
    {
        Debug.Log("Player died called");
        LevelManager.instance.GameOver();
        gameObject.SetActive(false);
    }

    private void OnGUI()
    {
        float t = Time.deltaTime / 1f;
        healthSlider.value = Mathf.Lerp(healthSlider.value, health, t);
    }
}
