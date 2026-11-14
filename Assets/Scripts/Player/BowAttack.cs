
using UnityEngine;
using UnityEngine.InputSystem;

public class BowAttack : MonoBehaviour
{
    [Header("弓の設定")]
    public float arrowSpeed = 30f;
    public float fireCooldown = 0.5f;
    public float damage = 25f;

    [Header("矢の設定")]
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;

    [Header("カメラ")]
    public Camera playerCamera;
    public float aimFieldOfView = 50f;

    private float nextFireTime;
    private float normalFieldOfView;
    private bool isAiming;

    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;

        if (playerCamera != null)
            normalFieldOfView = playerCamera.fieldOfView;
    }

    void Update()
    {
        if (Mouse.current == null) return;

        // 右クリックで弓を構える
        isAiming = Mouse.current.rightButton.isPressed;

        // カメラのズーム
        if (playerCamera != null)
        {
            float targetFOV = isAiming
                ? aimFieldOfView
                : normalFieldOfView;

            playerCamera.fieldOfView = Mathf.Lerp(
                playerCamera.fieldOfView,
                targetFOV,
                Time.deltaTime * 12f
            );
        }

        // 右クリックを離した瞬間に発射
        if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            FireArrow();
        }
    }

    void FireArrow()
    {
        if (Time.time < nextFireTime) return;

        if (arrowPrefab == null || arrowSpawnPoint == null)
        {
            Debug.LogWarning(
                "矢Prefabまたは発射位置が設定されていません！"
            );
            return;
        }

        nextFireTime = Time.time + fireCooldown;

        Quaternion spawnRotation = playerCamera != null
            ? playerCamera.transform.rotation
            : arrowSpawnPoint.rotation;

        GameObject arrow = Instantiate(
            arrowPrefab,
            arrowSpawnPoint.position,
            spawnRotation
        );

        ArrowProjectile projectile =
            arrow.GetComponent<ArrowProjectile>();

        if (projectile != null)
        {
            projectile.Initialize(
                arrowSpeed,
                damage
            );
        }
        else
        {
            Debug.LogError(
                "ArrowProjectileが矢Prefabにありません！"
            );
            Destroy(arrow);
        }
    }
}
