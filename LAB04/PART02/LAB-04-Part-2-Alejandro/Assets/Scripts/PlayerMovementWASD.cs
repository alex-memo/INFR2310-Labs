using UnityEngine;
/// <summary>
/// @alex-memo 2023
/// </summary>
public class PlayerMovementWASD : MonoBehaviour
{
    private CharacterController controller;
    private float speed = 5f;

    private const float turnSmoothTime = .2f;
    private float turnSmoothVelocity;

    private float jumpHeight = 4f;

    private Animator anim;

    private Transform cam;

    private Vector3 moveVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        movePlayer();
    }
    private void movePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 _dir= new Vector3(horizontal, 0f, vertical).normalized*speed;
        moveVelocity = new Vector3(_dir.x, moveVelocity.y, _dir.z);
        if (controller.isGrounded)
        {
            
            if (Input.GetButtonDown("Jump"))
            {
                moveVelocity.y = jumpHeight;
            }
        }
        else
        {
            moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }

        float targetAngle = Mathf.Atan2(moveVelocity.x, moveVelocity.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 movedir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        controller.Move(moveVelocity*Time.deltaTime);
      
    }

    private void walk()
    {
        if (controller.isGrounded)
        {
            animate(1f);
        }
    }
    private void idle()
    {
        if (controller.isGrounded)
        {
            animate(0);
        }
    }

    private void animate(float _value, float _dampTime = .1f)
    {
        anim.SetFloat("Speed", _value, _dampTime, Time.deltaTime);
    }
}
