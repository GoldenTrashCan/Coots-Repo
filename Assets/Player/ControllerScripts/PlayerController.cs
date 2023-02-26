using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Primary Variables")]
    public float groundSpeed;
    public float airSpeed;
    public float jumpPower;
    public int maxJumps;
    public Rigidbody rb;
    public Transform orientation;

    [Header("Gravity")]
    [Range(0, float.PositiveInfinity)]public float gravity;
    public float groundDrag;
    public float airDrag;

    [Header("Ground Check")]
    public bool isGrounded;
    public GameObject groundCheck;
    public float radius;
    public LayerMask layer;

    [Header("Debug Variables")]
    public int jumpsLeft;
    public float horizontalMovement;
    public float verticalMovement;
    public Vector3 moveDirection;

    public PlayerShit playerShit;
    public bool bossMode;
    public float maxSpeed;

    public Animator catAnimator;
    public GameObject mainCat;
    public GameObject cootFly;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        ControlDrag();
        CheckGround();

        catAnimator.SetBool("Move", moveDirection != Vector3.zero);

        if (!EnemySpawning.instance.cutscenePlaying && !playerShit.dead)
        {
            GetMoveInput();
            if(groundSpeed > maxSpeed)
            {
                groundSpeed = maxSpeed;
            }
        }
    }

    void FixedUpdate()
    {
        Move();
        Physics.gravity = new Vector3(0, -gravity, 0);
    }

    void ControlDrag()
    {
        if (isGrounded || bossMode)
            rb.drag = groundDrag;
        else
            rb.drag = airDrag / gravity;
    }

    void Move()
    {
        float speed;
        if (isGrounded || bossMode)
        {
            speed = groundSpeed;
        }
        else
        {
            speed = airSpeed;
        }

        rb.AddForce(moveDirection.normalized * speed, ForceMode.Acceleration);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        jumpsLeft--;
    }

    void GetMoveInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * y + orientation.right * x;

        if (Input.GetKey(KeyCode.E))
        {
            moveDirection = new Vector3(moveDirection.x, 1, moveDirection.z);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            moveDirection = new Vector3(moveDirection.x, -1, moveDirection.z);
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0 && !bossMode)
        {
            Jump();
        }

        if(rb.velocity != Vector3.zero)
        {
            mainCat.transform.rotation = Quaternion.RotateTowards(mainCat.transform.rotation, Quaternion.LookRotation(rb.velocity, Vector3.up), Time.deltaTime * 240f);
        }
    }

    void CheckGround()
    {
        bool check = Physics.CheckSphere(groundCheck.transform.position, radius, layer);

        if (check)
            jumpsLeft = maxJumps;

        isGrounded = check;
    }
}
