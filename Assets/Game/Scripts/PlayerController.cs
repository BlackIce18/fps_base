using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 4f;
    public float gravity = -20f;
    public float jumpHeight = 4f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.5f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Camera _camera;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Gravity();
        Move();
        Jump();
    }
    
    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        var camForward = _camera.transform.forward;
        var camRight = _camera.transform.right;

        camForward.y = 0;
        camRight.y = 0;
        
        camForward.Normalize();
        camRight.Normalize();

        Vector3 movement = camForward * z + camRight * x;
        Vector3 targetVelocity = movement * speed;
        Vector3 velocity = _rb.linearVelocity;
        
        if ((Input.GetAxis("Jump") > 0) && isGrounded)
        {
            velocity.y = jumpHeight;
        }
        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;
        _rb.linearVelocity = velocity;
    }

    void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (!isGrounded)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        velocity = velocity * Time.deltaTime;
    }
    
    private void Jump()
    {

    }
}
