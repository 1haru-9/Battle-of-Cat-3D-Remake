using UnityEngine;

public class Pebble : MonoBehaviour
{
    [Header("ホコリプレハブ")]
    public GameObject dustPrefab;

    [Header("消えるまでの時間")]
    public float destroyTime = 5f;

    private bool hasHit = false;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit)
            return;

        hasHit = true;

        // 接触地点にホコリを出す
        if (dustPrefab != null)
        {
            Instantiate(
                dustPrefab,
                collision.contacts[0].point,
                Quaternion.identity
            );
        }

        // 石を消す
        Destroy(gameObject);
    }
}