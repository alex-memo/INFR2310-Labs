using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// @alex-memo 2023
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;

    private float rotateVelocity;

    private readonly float rotateSpeedMovement = .5f;

    private Animator anim;

    [SerializeField] private LayerMask mask;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        move();
        anim.SetFloat("Speed", agent.velocity.magnitude>.1?1:0);
    }
    private void move()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, mask))
            {
                if (hit.transform.CompareTag("Ground"))
                {
                    moveToPoint(hit);
                }
            }
        }
    }
    private void moveToPoint(RaycastHit hit)
    {
        agent.SetDestination(hit.point);
        Quaternion _rotationLookAt = Quaternion.LookRotation(hit.point - transform.position);
        float _rotY = Mathf.SmoothDampAngle(transform.eulerAngles.y, _rotationLookAt.eulerAngles.y, ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));
        transform.eulerAngles = new(0, _rotY, 0);
    }
    private void OnTriggerEnter(Collider _coll)
    {
        if (_coll.CompareTag("Enemy"))
        {
            animate("Attack");
        }
    }
    private void OnTriggerExit(Collider _coll)
    {
        if (_coll.CompareTag("Enemy"))
        {
            animate("Walk");
        }
    }
    private void animate(string _trigger)
    {
        anim.SetTrigger(_trigger);
    }
}
