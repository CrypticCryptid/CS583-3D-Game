using System.Threading;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera fpsCam;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 10f; // bullets per second

    private float nextTimeToFire = 0f;

    public Transform muzzleFlare;
    public float flareRanInterval;
    private float ranInterval;

    bool isShooting;
    bool isReloading;

    Animator anim;

    public GameObject flashLight;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && !isReloading)
        {
            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 0.5f / fireRate;
                Shoot();
            }

            isShooting = true;
        } 
        else
        {
            isShooting = false;
        }

        if (Input.GetKeyDown(KeyCode.R) && !isShooting)
        {
           isReloading = true;
           anim.SetBool("isReloading", true); 
        }

        anim.SetBool("isShooting", isShooting);
        muzzleFlare.gameObject.SetActive(isShooting);

        if(Input.GetButtonDown("Fire2"))
        {
            flashLight.SetActive(!flashLight.activeInHierarchy);
        }
    }

    void LateUpdate()
    {
        if(ranInterval <= 0f)
        {
            Vector3 euler = muzzleFlare.transform.localEulerAngles;
            euler.y = Random.Range(0f, 360f);
            muzzleFlare.transform.localEulerAngles = euler;

            ranInterval = flareRanInterval;
        } 
        else
        {
            ranInterval -= Time.deltaTime;
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

    public void EndReloadAnim()
    {
        isReloading = false;
        anim.SetBool("isReloading", false);
    }
}
