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
    SpriteRenderer damagedObject;
    [SerializeField] Animator anim;
    [SerializeField] string tag;

    [HideInInspector] public bool isDead = false;

    void Start()
    {
        origHealth = damagePerSprite * (damagedStages.Length - 1);
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
            float threshold = origHealth * (1 - ((float)spriteStage + 1) / (damagedStages.Length - 1));
            Debug.Log("damage total is " + (1 - (spriteStage + 1) / (damagedStages.Length - 1)));
            // int threshold = (origHealth - (spriteStage + 1) * origHealth / damagePerSprite);
            Debug.Log("threshold is " + threshold);
            Debug.Log("object health is " + objectHealth);

            if ((objectHealth < threshold) && (spriteStage < (damagedStages.Length - 1)))
            {
                spriteStage++;
                UpdateSprite();
                if (spriteStage == damagedStages.Length)
                {
                    ExplodeObject();
                }
            }
        }
    }

    private void ExplodeObject()
    {
        // trigger anim "Explode"
        if (anim != null)
        {
            anim.SetTrigger("Explode");
        }
        if (tag != null)
        {
            objectPooler.SpawnFromPool("ExplosionMedium", transform.position, Quaternion.identity);            
        }

    }

    void UpdateSprite()
    {
        damagedObject.sprite = damagedStages[spriteStage];
        Debug.Log("Sprite updated to: " + spriteStage);
    } 
}
