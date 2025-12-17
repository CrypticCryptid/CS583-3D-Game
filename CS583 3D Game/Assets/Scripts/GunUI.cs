using UnityEngine;
using TMPro;   // <-- IMPORTANT for TextMeshPro

public class GunUI : MonoBehaviour
{
    public Gun gun;                 // reference to your Gun script
    public TMP_Text ammoText;       // TMP version of ammo text
    public TMP_Text reloadText;     // TMP version of reload indicator

    void Update()
    {
        if (gun == null) return;

        // Update ammo display "X / ∞"
        if (ammoText != null)
        {
            ammoText.text = gun.CurrentAmmo + " / ∞";
        }

        // Show or hide "Reloading..."
        if (reloadText != null)
        {
            reloadText.enabled = gun.IsReloading;

            if (gun.IsReloading)
                reloadText.text = "Reloading...";
        }
    }
}
