using UnityEngine;

public class Pebble : MonoBehaviour
{
    [Header("ホコリプレハブ")]
    public GameObject dustPrefab;

    [Header("消えるまでの時間")]
    public float destroyTime = 3f;

    private bool hasHit = false;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit)
            return;

        if (collision.gameObject.CompareTag("Ground"))
        {
            hasHit = true;

            if (dustPrefab != null)
            {
                Instantiate(
                    dustPrefab,
                    collision.contacts[0].point,
                    Quaternion.identity
                );
            }

            Destroy(gameObject);
        }
    }
}