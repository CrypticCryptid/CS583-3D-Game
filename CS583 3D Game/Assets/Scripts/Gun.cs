using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using TMPro;
using System.Collections;

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

    public PlayerStats stats;
    public TextMeshProUGUI ammoText;       // TMP version of ammo text
    public TextMeshProUGUI reloadText;     // TMP version of reload indicator

    Animator anim;

    public GameObject flashLight;

    public SpriteRenderer ammoBar;
    public Sprite[] ammoBarStages; //index 0 = full, index 9 = empty

    void Start()
    {
        anim = GetComponent<Animator>();
        stats = FindObjectOfType<PlayerStats>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && !isReloading && stats.GetCurrentAmmo() > 0)
        {
            if (Time.time >= nextTimeToFire)
            {
                StartCoroutine(ShootRoutine());
                nextTimeToFire = Time.time + 0.5f / fireRate;
                Shoot();
            }

            isShooting = true;
        }
        else if(Input.GetButton("Fire1") && stats.GetCurrentAmmo() <= 0 && !isReloading)
        {
            // Play empty clip sound
            FindObjectOfType<AudioManager>().PlayRanPitch("GunEmpty");
            isShooting = false;
        }
        else
        {
            isShooting = false;
        }

        if(Input.GetButtonDown("Fire2"))
        {
            FindObjectOfType<AudioManager>().PlayRanPitch("Flashlight");
            flashLight.SetActive(!flashLight.activeInHierarchy);
        }

        if (Input.GetKeyDown(KeyCode.R) && !isShooting)
        {
            FindObjectOfType<AudioManager>().PlayRanPitch("Reload");
            isReloading = true;
           anim.SetBool("isReloading", true); 
        }

        // Update ammo display "X / 8"
        ammoText.text = stats.GetCurrentAmmo() + " / ∞";
        reloadText.enabled = isReloading;

        UpdateAmmoBar();

        anim.SetBool("isShooting", isShooting);
        muzzleFlare.gameObject.SetActive(isShooting);
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

    IEnumerator ShootRoutine()
    {
        FindObjectOfType<AudioManager>().PlayRanPitch("LaserShot");
        yield return null;
    }
    
    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null || fpsCam == null)
        {
            Debug.LogWarning("Gun is missing references!");
            return;
        }

        // 1) Ray from the center of the screen (crosshair) to find where you're aiming
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
        {
            targetPoint = hit.point;          // exact point you are aiming at
        }
        else
        {
            // nothing hit: aim at a far point straight ahead
            targetPoint = fpsCam.transform.position + fpsCam.transform.forward * 1000f;
        }

        // 2) Direction from the gun's FirePoint TO that target point
        Vector3 direction = (targetPoint - firePoint.position).normalized;
        Quaternion bulletRotation = Quaternion.LookRotation(direction);

        // 3) Spawn a little in front of the muzzle so it's not inside enemies at close range
        float spawnOffset = 0.3f; // you can tweak this between 0.3–0.6
        Vector3 spawnPos = firePoint.position + direction * spawnOffset;

        // 4) Create bullet
        GameObject newBullet = Instantiate(bulletPrefab, spawnPos, bulletRotation);
        Bullet bullet = newBullet.GetComponent<Bullet>();

        if (bullet == null)
        {
            Debug.LogError("New bullet has no Bullet component!");
        }
        else if (stats == null)
        {
            Debug.LogError("Shooter has no PlayerStats!");
        }
        else
        {
            bullet.damage = stats.damage;
        }

        stats.ChangeAmmo(-1);
    }

    public void EndReloadAnim()
    {
        isReloading = false;
        stats.SetAmmoMax();
        anim.SetBool("isReloading", false);
    }

    void UpdateAmmoBar()
    {
        float fraction = (float)stats.GetCurrentAmmo() / stats.maxAmmo;
        int stage = Mathf.FloorToInt((1f - fraction) * ammoBarStages.Length);

        stage = Mathf.Clamp(stage, 0, ammoBarStages.Length - 1);
        ammoBar.sprite = ammoBarStages[stage];
    }
}
