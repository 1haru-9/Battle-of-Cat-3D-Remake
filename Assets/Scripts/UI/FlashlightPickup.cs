using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class FlashlightPickup : MonoBehaviour
{
    public Transform cameraTransform;
    public Light flashlightLight;
    public TextMeshProUGUI interactText;

    private bool canPickUp = false;
    private bool pickedUp = false;

    void Start()
    {
        interactText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (canPickUp && !pickedUp && Keyboard.current.eKey.wasPressedThisFrame)
        {
            PickUp();
        }
    }

    void PickUp()
    {
        pickedUp = true;

        // テキストを消す
        interactText.gameObject.SetActive(false);

        // カメラの子にする
        transform.SetParent(cameraTransform);

        // 左下オフハンド（後で調整）
        transform.localPosition = new Vector3(-0.28f, -0.22f, 0.72f);
        transform.localRotation = Quaternion.Euler(18f, 25f, -12f);

        // ライトを強くする
        flashlightLight.intensity = 8f;
        flashlightLight.range = 15f;
        flashlightLight.spotAngle = 60f;
        flashlightLight.innerSpotAngle = 42f;

        // コライダーを無効化
        GetComponent<Collider>().enabled = false;

        Debug.Log("懐中電灯を拾った！");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !pickedUp)
        {
            canPickUp = true;
            interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = false;
            interactText.gameObject.SetActive(false);
        }
    }
}