using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Core Stats")]

    public string weaponName;
    public Type type;
    public float baseDamage;
    public float attackSpeed;
    public float attackRange;
    public float criticalChance;
    public float criticalMultiplier;
    public float cooldown;

    [Header("Special Effects")]
    public float bonusArmor;
    public float armorPenetrationChance;
    public int lifestealOnKill;
    public float bonusMoveSpeed;

    [Header("Components")]
    public Transform firePoint;
    public GameObject projectilePrefab;
    public GameObject slashEffectPrefab;

    public HeroController player;
    public float shootTimer;
    public Camera mainCamera;
    public Transform aim;
    public enum Type { Sword, Bow, Dagger }

    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        shootTimer -= Time.deltaTime;

        AimAtMouse();
        if (Input.GetButton("Fire1") && shootTimer <= 0)
        {
            PrimaryAttack();
            shootTimer = cooldown;
        }

        if (Input.GetButton("Fire2"))
        {
            SecondaryAttack();
        }
    }




    public virtual void PrimaryAttack()
    {
        if ((projectilePrefab != null || slashEffectPrefab != null) && shootTimer <= 0f)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Mathf.Abs(mainCamera.transform.position.z);
            Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(mousePosition);
            Vector2 direction = (worldMousePos - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion rotationToMouse = Quaternion.Euler(0, 0, angle);

            if (projectilePrefab != null)
            {
                rotationToMouse = Quaternion.Euler(0, 0, angle - 45f);
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, rotationToMouse);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.velocity = direction * 10f;
            }

            if (slashEffectPrefab != null)
            {

            }

        }
    }

    private void AimAtMouse()
    {
        if (mainCamera == null || player == null) return;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(mainCamera.transform.position.z);

        Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(mousePosition);
        Vector2 direction = (worldMousePos - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        aim.rotation = Quaternion.Euler(0, 0, angle);


    }

    public virtual void SecondaryAttack()
    {
        
    }

    public virtual void OnEquip()
    {
        
    }

    public virtual void OnUnequip()
    {
        
    }

    public virtual void OnEnemyKill()
    {
        if (lifestealOnKill > 0 && player != null)
        {
            //player.Heal(lifestealOnKill);
        }
    }
}
