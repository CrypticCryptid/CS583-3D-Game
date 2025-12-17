using UnityEngine;
using TMPro;

public class GunUI : MonoBehaviour
{
    public PlayerStats stats;
    public TextMeshProUGUI ammoText;       // TMP version of ammo text
    public TextMeshProUGUI reloadText;     // TMP version of reload indicator

    void Start()
    {
        stats = FindObjectOfType<PlayerStats>();
    }

    void Update()
    {
        // // Update ammo display "X / ∞"
        // ammoText.text = stats.GetCurrentAmmo() + " / ∞";

        // reloadText.enabled = gun.IsReloading;

        // if (gun.IsReloading)
        //     reloadText.text = "Reloading...";
    }
}
