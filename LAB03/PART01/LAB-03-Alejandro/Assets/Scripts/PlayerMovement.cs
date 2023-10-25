using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// @alex-memo 2023
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    private NavMeshAgent agent;

    private Animator anim;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        agent.SetDestination(target.position);
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
