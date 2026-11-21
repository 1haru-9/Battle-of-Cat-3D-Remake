
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ArrowProjectile : MonoBehaviour
{
    private Rigidbody rb;

    private float damage;
    private bool initialized;
    private bool hasHit;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Initialize(float speed, float damageValue)
    {
        damage = damageValue;

        // 重力を有効化
        rb.useGravity = true;

        // 衝突検出
        rb.collisionDetectionMode =
            CollisionDetectionMode.ContinuousDynamic;

        // 初速を設定
        rb.linearVelocity = transform.forward * speed;

        initialized = true;

        // 5秒後に削除
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // 飛んでいる方向に矢の先端を向ける
        if (initialized && !hasHit && rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            transform.rotation =
                Quaternion.LookRotation(rb.linearVelocity);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!initialized || hasHit) return;

        hasHit = true;

        EnemyHealth enemy =
            collision.gameObject.GetComponentInParent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log("矢が敵に命中！ ダメージ：" + damage);
        }

        Destroy(gameObject);
    }
}
