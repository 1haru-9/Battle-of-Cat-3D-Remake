using System.Collections;
using UnityEngine;

public class PebbleSpawner : MonoBehaviour
{
    [Header("小石プレハブ")]
    public GameObject pebblePrefab;

    [Header("生成位置")]
    public Transform[] spawnPoints;

    [Header("生成間隔")]
    public float minInterval = 0.4f;
    public float maxInterval = 1.0f;

    [Header("小石サイズ")]
    public float minScale = 0.04f;
    public float maxScale = 0.08f;

    [Header("大きい石")]
    [Range(0f, 1f)]
    public float bigRockChance = 0.05f;

    public float bigRockMultiplier = 1.8f;

    [Header("色")]
    public Color color1 = Color.gray;
    public Color color2 = new Color(0.25f, 0.25f, 0.25f);
    public Color color3 = new Color(0.45f, 0.40f, 0.35f);

    [Header("消える時間")]
    public float destroyTime = 5f;

    [Header("崩落イベント")]
    [Range(0f, 1f)]
    public float collapseChance = 0.15f;

    public int collapseMin = 3;
    public int collapseMax = 5;

    [Header("崩落時差")]
    public float minDelay = 0.01f;
    public float maxDelay = 0.05f;

    private bool isRunning = false;

    void Start()
    {

    }

    public void StartFalling()
    {
        if (isRunning)
            return;

        isRunning = true;

        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (isRunning)
        {
            if (Random.value < collapseChance)
            {
                yield return StartCoroutine(Collapse());
            }
            else
            {
                SpawnSinglePebble();
            }

            yield return new WaitForSeconds(
                Random.Range(minInterval, maxInterval)
            );
        }
    }

    IEnumerator Collapse()
    {
        int amount = Random.Range(collapseMin, collapseMax + 1);

        for (int i = 0; i < amount; i++)
        {
            SpawnSinglePebble();

            yield return new WaitForSeconds(
                Random.Range(minDelay, maxDelay)
            );
        }
    }

    void SpawnSinglePebble()
    {
        if (spawnPoints.Length == 0)
            return;

        Transform point =
            spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject pebble =
            Instantiate(
                pebblePrefab,
                point.position,
                Random.rotation
            );

        float scale =
            Random.Range(minScale, maxScale);

        bool bigRock =
            Random.value < bigRockChance;

        if (bigRock)
        {
            scale *= bigRockMultiplier;
        }

        pebble.transform.localScale =
            Vector3.one * scale;

        Renderer renderer =
            pebble.GetComponent<Renderer>();

        if (renderer != null)
        {
            Color[] colors =
            {
                color1,
                color2,
                color3
            };

            renderer.material.color =
                colors[Random.Range(0, colors.Length)];
        }

        Destroy(pebble, destroyTime);
    }

    public void StopFalling()
    {
        isRunning = false;
    }
}