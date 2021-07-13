using System;
using UnityEngine;

public class DamageableObjects : MonoBehaviour
{
    public Sprite[] damagedStages;
    [Tooltip("Most damage at bottom of list")]
    public int damagePerSprite;

    private ObjectPooler objectPooler;
    private float objectHealth;
    private float origHealth;
    private int spriteStage;
    private SpriteRenderer damagedObject;
    private bool isDead = false;

    void Start()
    {
        origHealth = damagePerSprite * (damagedStages.Length);
        Debug.Log("Object Health is " + origHealth);
        objectHealth = origHealth;
        damagedObject = transform.GetComponent<SpriteRenderer>();
        objectPooler = ObjectPooler.Instance;
        spriteStage = 0;
        UpdateSprite();
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            objectHealth -= damage;
            float threshold = origHealth * ((damagedStages.Length - (float)spriteStage - 1 )/ damagedStages.Length);
            Debug.Log("threshold is " + threshold);
            Debug.Log("object health is " + objectHealth);

            if ((objectHealth <= threshold) && (spriteStage < (damagedStages.Length-1)))
            {
                spriteStage++;
                UpdateSprite();
                if (spriteStage == damagedStages.Length-1)
                {
                    Debug.Log("Trying to start explosion");
                    gameObject.GetComponentInChildren<ExplodeObject>(true).StartExplosion();
                    isDead = true;
                    Debug.Log("isDead is true");
                }
            }
        }
    }

    void UpdateSprite()
    {
        damagedObject.sprite = damagedStages[spriteStage];
        Debug.Log("Sprite updated to: " + spriteStage);
    } 
}
