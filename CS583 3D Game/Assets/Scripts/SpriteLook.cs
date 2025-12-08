using UnityEngine;

public class SpriteLook : MonoBehaviour
{
    private Transform target;
    public bool canLookVertically; //if TRUE, the sprite will ALWAYS appear flat to the camera

    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        //this if statement calculates how the sprite should appear to the camera based on the bool
        if(canLookVertically)
        {
            transform.LookAt(target);
        }
        else
        {
            Vector3 modifiedTarget = target.position;
            modifiedTarget.y = transform.position.y;
            
            transform.LookAt(modifiedTarget);
        }
    }
}
