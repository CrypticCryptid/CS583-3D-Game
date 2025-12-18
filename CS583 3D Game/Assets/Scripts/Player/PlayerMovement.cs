using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;

    private PlayerStats Stats;

    public float speed;
    
    [SerializeField]
    private float gravity = -9.81f;
    [SerializeField]
    private float jumpHeight = 3f;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundDistance = 0.4f;
    [SerializeField]
    private LayerMask groundMask;
    [SerializeField]
    private float restingVal = -3f; //needs to be negative because gravity is down

    Vector3 fallVelocity;
    bool  isGrounded;

    private void Start()
    {
        Stats = GetComponent<PlayerStats>();
        speed = Stats.speed;
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //detects if the player is near a "Ground"-layer object

        if(isGrounded && fallVelocity.y < 0) //used to apply a slight gravity-like effect when on the ground
        {
            fallVelocity.y = restingVal;
        }

        //get controls for player
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //apply the controls on a local scale
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //allows player to jump, using physics
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            FindObjectOfType<AudioManager>().Play("PlayerJump");
            fallVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //calculates gravity
        fallVelocity.y += gravity * Time.deltaTime;
        controller.Move(fallVelocity * Time.deltaTime);
    }
}
