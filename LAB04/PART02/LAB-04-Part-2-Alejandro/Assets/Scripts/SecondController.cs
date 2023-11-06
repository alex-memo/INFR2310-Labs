using UnityEngine;
/// <summary>
/// @alex-memo 2023
/// </summary>
public class SecondController : PathController
{
    protected override void OnTriggerEnter(Collider _coll)
    {
        toggleWalk();
    }
}
