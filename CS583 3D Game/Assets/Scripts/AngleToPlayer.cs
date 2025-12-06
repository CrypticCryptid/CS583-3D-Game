using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleToPlayer : MonoBehaviour
{
    private Transform player;
    private Vector3 targetPos;
    private Vector3 targetDir;

    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    public float angle;
    public int lastIndex;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        targetDir = targetPos - transform.position;

        angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

        // Vector3 tempScale = Vector3.one;
        // if(angle > 0)
        // {
        //     tempScale.x *= -1f;
        // }

        // spriteRenderer.transform.localScale = tempScale;

        lastIndex = GetIndex(angle);

        spriteRenderer.sprite = sprites[lastIndex];
    }

    private int GetIndex(float angle)
    {
        if(angle >= -45f && angle <= 45f) //front
            return 0;
        if(angle > -135f && angle < -45f) //right
            return 1;
        if(angle <= -135f || angle >= 135f) //back
            return 2;
        if(angle > 45f && angle < 135f) //left
            return 3;
        
        return lastIndex;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, targetPos);
    }
}
