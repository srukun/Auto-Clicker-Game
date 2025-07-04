using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject currentWeaponObject;
    public GameObject[] weaponPrefabs;

    public GameObject nearbyWeapon;
    public SceneManager sceneManager;
    public GameObject weaponDrop;
    void Update()
    {
        Interact();
    }
    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (nearbyWeapon != null)
            {
                if (currentWeaponObject != null)
                {
                    DropWeapon(nearbyWeapon.transform, currentWeaponObject.GetComponentInChildren<SteelsEdge>().weaponName);
                }
                PickupWeapon(nearbyWeapon);
            }
        }
    }

    public void SetNearbyWeapon(GameObject weapon)
    {
        nearbyWeapon = weapon;
    }

    public void ClearNearbyWeapon(GameObject weapon)
    {
        if (nearbyWeapon == weapon)
        {
            nearbyWeapon = null;
        }
    }

    private void PickupWeapon(GameObject weaponPickup)
    {

        GameObject weapon = null;
        if (weaponPickup.GetComponent<WeaponDropManager>().weaponName == "Guardians Sword")
        {
            weapon = Instantiate(weaponPrefabs[0], Vector3.zero, Quaternion.identity, gameObject.transform);
        }
        if (weaponPickup.GetComponent<WeaponDropManager>().weaponName == "Sheepskin Bow")
        {
            weapon = Instantiate(weaponPrefabs[1], Vector3.zero, Quaternion.identity, gameObject.transform);
        }
        if (weaponPickup.GetComponent<WeaponDropManager>().weaponName == "Steels Edge")
        {
            weapon = Instantiate(weaponPrefabs[2], Vector3.zero, Quaternion.identity, gameObject.transform);
        }
        
        if(weapon != null)
        {
            weapon.transform.localPosition = Vector3.zero;
        }
        Destroy(weaponPickup.gameObject);
        currentWeaponObject = weapon;
        sceneManager.WeaponSetup(currentWeaponObject);

    }

    private void DropWeapon(Transform nearbyWeaponTransform, string currentWeaponObjectName)
    {
        if (currentWeaponObject != null)
        {
            GameObject dropped = Instantiate(weaponDrop, nearbyWeaponTransform.position, Quaternion.identity);
            dropped.GetComponent<WeaponDropManager>().weaponName = currentWeaponObjectName;
            Destroy(currentWeaponObject);
            currentWeaponObject = null;
        }
    }

}
