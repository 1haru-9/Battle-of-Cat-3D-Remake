using UnityEngine;

public class CollapseEvent : MonoBehaviour
{
    [Header("落石スポナー")]
    public PebbleSpawner pebbleSpawner;

    [Header("カメラシェイク")]
    public CameraShake cameraShake;

    [Header("効果音")]
    public AudioSource audioSource;
    public AudioClip collapseSound;

    [Header("カメラ揺れ設定")]
    public float shakeTime = 0.4f;
    public float shakePower = 0.08f;

    private bool hasStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        // 一度だけ実行
        if (hasStarted)
            return;

        // プレイヤー以外は反応しない
        if (!other.CompareTag("Player"))
            return;

        hasStarted = true;

        // カメラを揺らす
        if (cameraShake != null)
        {
            cameraShake.Shake(shakeTime, shakePower);
        }

        // 崩落音を鳴らす
        if (audioSource != null && collapseSound != null)
        {
            audioSource.PlayOneShot(collapseSound);
        }

        // 落石開始
        if (pebbleSpawner != null)
        {
            pebbleSpawner.StartFalling();
        }
    }
}