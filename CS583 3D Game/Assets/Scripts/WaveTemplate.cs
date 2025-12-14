using UnityEngine;

[CreateAssetMenu(menuName = "Wave Template")]
public class WaveTemplate : ScriptableObject
{
    public Vector3 bounds; //x = common bound, y = uncommon bound, z = rare bound

    public GameObject[] cEnemies;
    public GameObject[] uEnemies;
    public GameObject[] rEnemies;
    public GameObject[] srEnemies;

    public float enemiesAmount;
}
