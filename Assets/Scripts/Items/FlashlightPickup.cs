using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightPickup : MonoBehaviour
{
    public Transform cameraTransform;
    public Light flashlightLight;

    private bool canPickUp = false;
    public bool pickedUp = false;

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

        transform.SetParent(cameraTransform, false);

        // 左下オフハンド位置
        transform.localPosition = new Vector3(-1.3f, -0.6f, 0.45f);

        // 左手で持ってる感じ
        transform.localRotation = Quaternion.identity;

        GetComponent<Collider>().enabled = false;

        // 【追加】懐中電灯UIに対して「画像を表示して」と命令を出す
        if (FlashlightUI.Instance != null)
        {
            FlashlightUI.Instance.ShowIcon();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !pickedUp)
        {
            canPickUp = true;
            Debug.Log("Eで懐中電灯を拾う");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = false;
        }
    }
}
