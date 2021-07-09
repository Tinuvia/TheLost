using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    //[SerializeField] GameObject projectilePrefab; Not in use?
    public Transform firePoint;
    public float fireForce;
    public int ammo;
    public TextMeshProUGUI ammoText;
    private Animator animator;
    private int isFiringHash;
    private ObjectPooler objectPooler;

    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        animator = GetComponent<Animator>();
        ammoText.text = ammo.ToString();
        isFiringHash = Animator.StringToHash("IsFiring");
    }

    private void Update() {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("PauseMenu")) // checking to make sure we aren't in the pause menu
        {            
            if(Input.GetMouseButtonDown(0)) {
                Fire();
            }
            if (Input.GetMouseButtonUp(0)) {
                StopFiring();
            }
        } else
            Debug.Log("We are in the pause menu");

    }

    public void Fire() {
        if (ammo > 0) {       
            GameObject pooledProjectile = objectPooler.SpawnFromPool("Projectile", firePoint.position, firePoint.rotation); // Brackey

            if (pooledProjectile != null)
            {
                pooledProjectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
                UpdateAmmo(-1);
                StartCoroutine("SetFalse", pooledProjectile);
                animator.SetBool(isFiringHash, true);
                Debug.Log("Projectile fired");
            }
        }
    }

    public void StopFiring() {
        animator.SetBool(isFiringHash, false);
    }

    public void UpdateAmmo(int mod)
    {
        ammo += mod;
        ammoText.text = ammo.ToString();
        Debug.Log("Ammo count " + ammo);
    }

    IEnumerator SetFalse(GameObject objectToDisable) {

        yield return new WaitForSeconds(3.0f); // disable projectile after 1.0f secs
        if(objectToDisable.activeSelf)  {
            objectToDisable.SetActive(false);
            Debug.Log("Projectile disabled from time");
        }
    }
}
