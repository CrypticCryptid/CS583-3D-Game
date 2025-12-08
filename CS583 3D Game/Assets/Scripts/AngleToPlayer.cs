using UnityEngine;

public class AngleToPlayer : MonoBehaviour
{
    private Transform player;
    private Vector3 targetPos;
    private Vector3 targetDir;

    //if object has no animations, use fixedSprites
    private SpriteRenderer spriteRenderer;
    public Sprite[] fixedSprites;

    private float angle; //angle to player
    public int lastIndex; //previous facing direction

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        targetDir = targetPos - transform.position;

        angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up); //calculates angle to player

        //[!!!] Used to flip Sprite

        // Vector3 tempScale = Vector3.one;
        // if(angle > 0)
        // {
        //     tempScale.x *= -1f;
        // }

        // spriteRenderer.transform.localScale = tempScale;

        lastIndex = GetIndex(angle);

        if(fixedSprites.Length > 0)
            spriteRenderer.sprite = fixedSprites[lastIndex];
    }

    //This function returns the index associated with the direction that the object is facing and the side that should be showing to the camera
    private int GetIndex(float angle)
    {
        if(angle >= -45f && angle <= 45f) //front
            return 0;
        if(angle > -135f && angle < -45f) //left
            return 1;
        if(angle <= -135f || angle >= 135f) //back
            return 2;
        if(angle > 45f && angle < 135f) //right
            return 3;
        
        return lastIndex;
    }

    //[NOTE] the below function helps with checking angles if needed
    // void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawRay(transform.position, transform.forward);

    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawLine(transform.position, targetPos);
    // }
}
