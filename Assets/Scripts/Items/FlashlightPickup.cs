using UnityEngine;

public class FlashlightPickup : MonoBehaviour
{
    public Transform cameraTransform;
    public Light flashlightLight;

    private bool canPickUp = false;
    private bool pickedUp = false;

    void Update()
    {
        if (canPickUp && !pickedUp && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        pickedUp = true;

        // カメラの子にする
        transform.SetParent(cameraTransform);

        // 持った位置
        transform.localPosition = new Vector3(0.25f, -0.18f, 0.55f);

        // 持った向き
        transform.localRotation = Quaternion.Euler(0f, 180f, 0f);

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