using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPos;

    void Awake()
    {
        originalPos = transform.localPosition;
    }

    public IEnumerator Shake(float duration, float strength)
    {
        float timer = 0f;

        while (timer < duration)
        {
            Vector3 offset = Random.insideUnitSphere * strength;
            offset.z = 0f;

            transform.localPosition = originalPos + offset;

            timer += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}