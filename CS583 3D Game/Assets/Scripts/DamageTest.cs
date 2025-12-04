using UnityEngine;

public class DamageTest : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            EnemyStats enemy = GetComponent<EnemyStats>();
            if(enemy != null)
            {
                enemy.takeDamage(20f);
                
                Debug.Log("Enemy took 20 damage. Current Health: " + enemy.currentHealth);
            }
            else
            {
                Debug.Log("No EnemyStats component found on this GameObject.");
            }
        }
    }
}
