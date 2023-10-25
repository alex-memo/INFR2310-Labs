using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// @alex-memo 2023
/// </summary>
public class PathController : MonoBehaviour
{
    [SerializeField] private PathManager pathManager;
    private List<Waypoint> waypoints;
    private Waypoint target;

    private float moveSpeed=1;
    private float rotateSpeed=15;

    private Animator animator;
    private bool isWalking;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        setAnimatorWalkingState();

        waypoints = pathManager.GetPath();
        if (waypoints != null && waypoints.Count > 0)
        {
            target = waypoints[0];
        }
    }
    private void Update()
    {
        if(Input.anyKeyDown)
        {
            toggleWalk();
        }
        if(!isWalking)
        {
            return;
        }
        rotateTowardsTarget();
        moveForward();
    }
    private void rotateTowardsTarget()
    {
        float _stepSize = rotateSpeed * Time.deltaTime;

        Vector3 _direction = (target.Position - transform.position);
        Vector3 _newDir = Vector3.RotateTowards(transform.forward, _direction, _stepSize, 0);

        transform.rotation = Quaternion.LookRotation(_newDir);
    }
    private void moveForward()
    {
        float _stepSize = moveSpeed * Time.deltaTime;
        float _distanceTotarget = Vector3.Distance(transform.position, target.Position);
        if (_distanceTotarget < _stepSize)
        {
            return;
        }
        Vector3 _moveDir = Vector3.forward;
        transform.Translate(_moveDir * _stepSize);
    }

    protected virtual void OnTriggerEnter(Collider _coll)
    {
        target = pathManager.GetNextTarget();
    }
    private void setAnimatorWalkingState()
    {
        animator.SetBool("IsWalking", isWalking);
    }
    protected void toggleWalk()
    {
        isWalking = !isWalking;
        setAnimatorWalkingState();
    }
}
