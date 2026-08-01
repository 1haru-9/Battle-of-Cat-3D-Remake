using UnityEngine;
using System.Collections;

public class CollapseEvent : MonoBehaviour
{
    [Header("カメラシェイク")]
    public CameraShake cameraShake;

    [Header("落石スポナー")]
    public PebbleSpawner pebbleSpawner;

    [Header("開始待機（プレイヤーに状況を理解させる）")]
    public float minStartDelay = 1f;
    public float maxStartDelay = 5f;

    [Header("余震設定")]
    public float minInterval = 8f;
    public float maxInterval = 18f;

    private bool playerInside = false;
    private bool collapseStarted = false;
    private Coroutine aftershockCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = true;

        // 最初の崩落は一度だけ
        if (!collapseStarted)
        {
            collapseStarted = true;
            StartCoroutine(StartCollapseWithDelay());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = false;

        // 部屋から出たら余震停止
        if (aftershockCoroutine != null)
        {
            StopCoroutine(aftershockCoroutine);
            aftershockCoroutine = null;
        }
    }

    IEnumerator StartCollapseWithDelay()
    {
        // 1〜5秒待つ
        float delay = Random.Range(minStartDelay, maxStartDelay);
        yield return new WaitForSeconds(delay);

        // プレイヤーがもう部屋にいないなら中止
        if (!playerInside)
            yield break;

        // 最初は小さめの揺れ
        if (cameraShake != null)
        {
            yield return StartCoroutine(
                cameraShake.Shake(0.25f, 0.08f)
            );
        }

        // 小石を少し落とす
        if (pebbleSpawner != null)
        {
            pebbleSpawner.StartFalling();
        }

        // 余震ループ開始
        if (aftershockCoroutine == null)
        {
            aftershockCoroutine = StartCoroutine(AftershockLoop());
        }
    }

    IEnumerator AftershockLoop()
    {
        while (playerInside)
        {
            // 8〜18秒ランダム待機
            yield return new WaitForSeconds(
                Random.Range(minInterval, maxInterval)
            );

            // プレイヤーが出ていたら終了
            if (!playerInside)
                yield break;

            // 少し強めの余震
            float strength = Random.Range(0.12f, 0.20f);
            float duration = Random.Range(0.25f, 0.45f);

            if (cameraShake != null)
            {
                yield return StartCoroutine(
                    cameraShake.Shake(duration, strength)
                );
            }

            // 小石を落とす
            if (pebbleSpawner != null)
            {
                pebbleSpawner.StartFalling();
            }
        }

        aftershockCoroutine = null;
    }
}