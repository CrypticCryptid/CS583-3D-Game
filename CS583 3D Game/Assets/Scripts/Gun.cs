using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera fpsCam;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 5f; // bullets per second

    private float nextTimeToFire = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null || fpsCam == null)
        {
            Debug.LogWarning("Gun is missing references!");
            return;
        }

        // Use camera direction so bullet goes where you're looking
        Transform camT = fpsCam.transform;
        Quaternion bulletRotation = Quaternion.LookRotation(camT.forward);

        Instantiate(bulletPrefab, firePoint.position, bulletRotation);
    }
}