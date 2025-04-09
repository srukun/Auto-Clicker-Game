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

    public enum Type { Sword, Bow, Dagger }

    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        Aim();
        {
            shootTimer -= Time.deltaTime;
        }
        AimAtMouse();
        if (Input.GetButton("Fire1"))
        {
            TryPrimaryAttack();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            SecondaryAttack();
        }
    }

    public virtual void Aim()
    {
        if (firePoint == null || mainCamera == null) return;

        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - firePoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firePoint.eulerAngles = new Vector3(0f, 0f, angle - 90f);
    }

    public virtual void TryPrimaryAttack()
    {
        if (shootTimer <= 0f)
        {
            PrimaryAttack();
            shootTimer = cooldown;
        }
    }

    public virtual void PrimaryAttack()
    {
        if (projectilePrefab != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * 10f, ForceMode2D.Impulse);
            
        }
        else if (slashEffectPrefab != null)
        {
            Instantiate(slashEffectPrefab, firePoint.position, firePoint.rotation);
            
        }
    }
    private void AimAtMouse()
    {
        if (mainCamera == null || player == null) return;

        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - player.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 45f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
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
