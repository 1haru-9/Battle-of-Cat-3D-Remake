
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class SwordAttack : MonoBehaviour
{
    [Header("攻撃設定")]
    public float damage = 25f;
    public float attackRange = 2.2f;
    public float attackRadius = 0.8f;
    public float attackCooldown = 0.8f;

    [Header("カメラ")]
    public Camera playerCamera;

    [Header("剣の設定")]
    public Transform swordTransform;

    [Header("攻撃モーション")]
    public float swingDuration = 0.38f;
    public float returnDuration = 0.22f;
    public float swingAngle = 90f;

    private float nextAttackTime = 0f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private bool initialized = false;
    private bool isAttacking = false;

    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;

        if (swordTransform != null)
        {
            originalPosition = swordTransform.localPosition;
            originalRotation = swordTransform.localRotation;
            initialized = true;
        }
    }

    void Update()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Attack();
        }
    }

    void Attack()
    {
        if (Time.time < nextAttackTime) return;
        if (isAttacking) return;

        nextAttackTime = Time.time + attackCooldown;

        if (playerCamera == null) return;

        if (initialized)
            StartCoroutine(SwingSword());

        Vector3 attackPosition =
            playerCamera.transform.position +
            playerCamera.transform.forward * attackRange * 0.5f;

        Collider[] hits = Physics.OverlapSphere(
            attackPosition,
            attackRadius
        );

        HashSet<EnemyHealth> damagedEnemies =
            new HashSet<EnemyHealth>();

        foreach (Collider hit in hits)
        {
            EnemyHealth enemy =
                hit.GetComponentInParent<EnemyHealth>();

            if (enemy != null && damagedEnemies.Add(enemy))
            {
                enemy.TakeDamage(damage);

                Debug.Log(
                    "剣で攻撃！ ダメージ：" + damage
                );
            }
        }

        Debug.Log("剣を振りました！");
    }

    IEnumerator SwingSword()
    {
        isAttacking = true;

        Quaternion startRotation = originalRotation;

        Quaternion attackRotation =
            originalRotation *
            Quaternion.Euler(0f, swingAngle, 0f);

        // 横薙ぎ
        float elapsed = 0f;

        while (elapsed < swingDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(
                elapsed / swingDuration
            );

            swordTransform.localRotation =
                Quaternion.Slerp(
                    startRotation,
                    attackRotation,
                    t
                );

            yield return null;
        }

        // 元の角度に戻す
        elapsed = 0f;

        while (elapsed < returnDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(
                elapsed / returnDuration
            );

            swordTransform.localRotation =
                Quaternion.Slerp(
                    attackRotation,
                    startRotation,
                    t
                );

            yield return null;
        }

        swordTransform.localPosition = originalPosition;
        swordTransform.localRotation = originalRotation;

        isAttacking = false;
    }
}
