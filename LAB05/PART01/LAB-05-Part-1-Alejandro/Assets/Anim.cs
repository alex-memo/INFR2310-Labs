using UnityEngine;
/// <summary>
/// @alex-memo 2023
/// </summary>
public class Anim : MonoBehaviour
{
    public Transform[] FootTarget;
    public Transform HandTarget;
    public Transform Attraction;
    public Transform HandPole;
    public Transform LookTarget;
    private void LateUpdate()
    {
        for (int i = 0; i < FootTarget.Length; i++)
        {
            var foot = FootTarget[i];
            var ray = new Ray(foot.transform.position + Vector3.up * 0.5f, Vector3.down);
            var hitInfo = new RaycastHit();
            if (Physics.SphereCast(ray, 0.05f, out hitInfo, 0.50f))
                foot.position = hitInfo.point + Vector3.up * 0.05f;
        }

        var normDist = Mathf.Clamp((Vector3.Distance(LookTarget.position, Attraction.position) - 0.3f) / 1f, 0, 1);
        HandTarget.rotation = Quaternion.Lerp(Quaternion.Euler(90, 0, 0), HandTarget.rotation, normDist);
        HandTarget.position = Vector3.Lerp(Attraction.position, HandTarget.position, normDist);
        HandPole.position = Vector3.Lerp(HandTarget.position + Vector3.down * 2, HandTarget.position + Vector3.forward * 2f, normDist);

    }
}
