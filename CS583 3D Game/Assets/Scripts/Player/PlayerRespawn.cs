using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 RespawnPoint;
    public void Respawn()
    {
        transform.position = RespawnPoint;
    }
}
