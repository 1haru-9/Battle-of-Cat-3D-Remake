
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ArrowProjectile : MonoBehaviour
{
    private Rigidbody rb;
    private float damage;
    private bool initialized;
    private bool hitSomething;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Initialize(float speed, float damageValue)
    {
        damage = damageValue;

        rb.useGravity = false;
        rb.collisionDetectionMode =
            CollisionDetectionMode.ContinuousDynamic;

        rb.linearVelocity = transform.forward * speed;

        initialized = true;

        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!initialized || hitSomething) return;

        hitSomething = true;

        EnemyHealth enemy =
            collision.collider.GetComponentInParent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log("矢が命中！ ダメージ：" + damage);
        }

        Destroy(gameObject);
    }
}
