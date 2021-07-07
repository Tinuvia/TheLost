using UnityEngine;

public class DamageableObjects : MonoBehaviour
{
    public Sprite[] damagedStages;
    [Tooltip("Most damage at bottom of list")]
    public int damagePerSprite;

    private float objectHealth;
    private float origHealth;
    private int spriteStage;
    SpriteRenderer damagedObject;

    void Start()
    {
        origHealth = damagePerSprite * damagedStages.Length;
        Debug.Log("Object Health is " + origHealth);
        objectHealth = origHealth;
        damagedObject = transform.GetComponent<SpriteRenderer>();
        spriteStage = 0;
        UpdateSprite();
    }

    public void TakeDamage(float damage)
    {
        objectHealth -= damage;
        float threshold = origHealth * (1 - ((float)spriteStage + 1) / (damagedStages.Length - 1));
        Debug.Log("damage total is " + (1 - (spriteStage + 1) / (damagedStages.Length - 1)));
        // int threshold = (origHealth - (spriteStage + 1) * origHealth / damagePerSprite);
        Debug.Log("threshold is " + threshold);
        Debug.Log("object health is " + objectHealth);

        if ((objectHealth < threshold) && (spriteStage < (damagedStages.Length-1)))
        {
            spriteStage++;
            UpdateSprite();
        }
    }

    void UpdateSprite()
    {
        damagedObject.sprite = damagedStages[spriteStage];
        Debug.Log("Sprite updated to: " + spriteStage);
    }

 
}
