using System.Collections;
using UnityEngine;
/// <summary>
/// @alex-memo 2023
/// </summary>
public class BarrelScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float lerpSpeed = 4.0f; // The speed of the interpolation 
    private float fixedDistance = 5;
    private bool isAnimating = false; // A flag to check if the object is already animating
    private bool isPlayerInRange;

    private void OnTriggerStay(Collider _coll)
    {
        if (_coll.CompareTag("Player"))
        {
            //isPlayerInRange = true;
            if (isAnimating) { return; }
            StartCoroutine(lerpToPosition(_coll.transform.position));
        }
    }
    private IEnumerator lerpToPosition(Vector3 playerPosition)
    {
        isAnimating = true;
        float duration = 4f;
        float timer = 0f;
        Vector3 startPosition = transform.position;
        Vector3 directionToPlayer = (playerPosition - transform.position).normalized;
        Vector3 endPosition = playerPosition + directionToPlayer * fixedDistance;
        endPosition.y = transform.position.y; // Keeps the barrel at the same y position

        while (timer < duration)
        {
            float t = timer / duration;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition; // Ensure it ends up exactly at the end position
        yield return new WaitForSeconds(1f);
        isAnimating = false;
    }
}
